/****
 * Created By: Jacob Sharp
 * Date Created: Feb 17, 2022
 * 
 * Last Edited By: Jacob Sharp
 * Date Last Edited: Feb 17, 2022
 * 
 * Description: Acts as a game manager
 ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    private static MissionDemolition S; // a private singleton

    [Header("SET IN INSPECTOR")]
    public Text uitLevel; // level text
    public Text uitShots; // shot count text
    public Text uitButton; // button text
    public Vector3 castlePos; // position to place castles
    public GameObject[] castles; // an array of castle prefabs

    [Header("SET DYNAMICALLY")]
    public int level; // current level
    public int levelMax; // number of levels
    public int shotsTaken; // count of shots taken
    public GameObject castle; // currently spawned castle
    public GameMode mode = GameMode.idle; // current game mode
    public string showing = "Show Slingshot"; // mode for FollowCam

    private void Start()
    {
        S = this; // set singleton

        level = 0; // set level information
        levelMax = castles.Length;

        StartLevel(); // start first level
    }

    void StartLevel() // start a new level
    {
        if (castle != null) Destroy(castle); // destroy old castle

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile"); // destroy old projectiles
        foreach (GameObject pTemp in gos) Destroy(pTemp);

        castle = Instantiate<GameObject>(castles[level]); // create new castle
        castle.transform.position = castlePos;

        shotsTaken = 0; // reset shot count

        SwitchView("Show Both"); // reset camera

        ProjectileLine.S.Clear(); // remove old projectile lines

        Goal.goalMet = false; // reset goal
        
        UpdateGUI(); // refresh GUI text
        
        mode = GameMode.playing; // update game mode
    }

    void UpdateGUI() // update GUI text
    {
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }

    private void Update()
    {
        UpdateGUI(); // update GUI text each frame

        if (mode == GameMode.playing && Goal.goalMet) // handle level end and start new level
        {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel() // start new level
    {
        level++;
        if (level == levelMax) level = 0;
        StartLevel();
    }

    public void SwitchView(string eView = "") // change view of camera when button is pressed
    {
        if (eView == "") eView = uitButton.text;
        showing = eView;

        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.target = null;
                uitButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.target = castle;
                uitButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.target = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }

    public static void ShotFired() { S.shotsTaken++; } // allow shot count to be incremented from anywhere
}
