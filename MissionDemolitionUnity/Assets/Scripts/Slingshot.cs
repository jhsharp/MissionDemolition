/****
 * Created By: Jacob Sharp
 * Date Created: Feb 9, 2022
 * 
 * Last Edited By: Jacob Sharp
 * Date Last Edited: Feb 14, 2022
 * 
 * Description: Manages the slingshot and launches projectiles
 ****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("SET IN INSPECTOR")]
    public GameObject prefabProjectile;
    public float velocityMultiplier = 8f;

    [Header("SET DYNAMICALLY")]
    public GameObject launchPoint;
    public Vector3 launchPos; // position projectiles are launched from
    public GameObject projectile; // instance of projectile
    public Rigidbody projectileRB; // rigidbody of projectile instance
    public bool aimingMode; // indicates whether the player is currently aiming
    
    private void Awake()
    {
        launchPoint = GameObject.Find("LaunchPoint"); // find child object
        launchPos = launchPoint.transform.position;
        launchPoint.SetActive(false); // deactivate halo at start
    }

    private void Update()
    {
        if (!aimingMode) return; // don't update if the player is aiming

        Vector3 mousePos2D = Input.mousePosition; // get the current position of the mouse in 2d coordinates
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D); // convert the mouse coordinates to world space

        Vector3 mouseDelta = mousePos3D - launchPos; // distance between mouse position and launch position
        float maxMagnitude = GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude) // limit mouseDelta to radius of the slingshot aiming sphere
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        projectile.transform.position = launchPos + mouseDelta; // move projectile to mouse position (within sphere)

        if (Input.GetMouseButtonUp(0)) // fire the projectile when the left mouse button is released
        {
            aimingMode = false;
            projectileRB.isKinematic = false;
            projectileRB.velocity = -mouseDelta * velocityMultiplier; // launch projectile
            
            FollowCam.target = projectile; // set camera to follow projectile
            projectile = null;
        }
    }

    private void OnMouseEnter()
    {
        launchPoint.SetActive(true); // activate halo
        Debug.Log("MouseEnter on Slingshot");
    }

    private void OnMouseExit()
    {
        launchPoint.SetActive(false); // turn off halo
        Debug.Log("MouseExit on Slingshot");
    }

    private void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate<GameObject>(prefabProjectile, launchPos, launchPoint.transform.rotation); // create projectile from prefab
        projectile.GetComponent<Rigidbody>().isKinematic = true; // don't apply physics to projectile yet
        projectileRB = projectile.GetComponent<Rigidbody>(); // get rigidbody of projectile instance
    }
}
