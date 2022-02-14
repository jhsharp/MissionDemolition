/****
 * Created By: Jacob Sharp
 * Date Created: Feb 14, 2022
 * 
 * Last Edited By: Jacob Sharp
 * Date Last Edited: Feb 14, 2022
 * 
 * Description: Makes camera follow the projectile
 ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Header("SET IN INSPECTOR")]
    public static GameObject target; // the static point of interest
    public float easing = 0.5f;
    public Vector2 minXY = Vector2.zero;

    [Header("SET DYNAMICALLY")]
    public float camZ; // set Z position of camera

    private void Awake()
    {
        camZ = transform.position.z;
    }

    private void FixedUpdate()
    {
        if (target == null) return; // do nothing if there is no target to follow

        Vector3 destination = target.transform.position; // set camera to focus on target object

        destination.x = Mathf.Max(minXY.x, destination.x); // clamp x and y
        destination.y = Mathf.Max(minXY.y, destination.y);
        
        destination = Vector3.Lerp(transform.position, destination, easing); // interpolate from current position to destination
        destination.z = camZ;
        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10; // zoom out as projectile rises
    }
}
