/****
 * Created By: Jacob Sharp
 * Date Created: Feb 17, 2022
 * 
 * Last Edited By: Jacob Sharp
 * Date Last Edited: Feb 17, 2022
 * 
 * Description: React when the goal is hit by a projectile
 ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static bool goalMet = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile") // activate the goal zone when a projectile enters it
        {
            Goal.goalMet = true;
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }
    }
}
