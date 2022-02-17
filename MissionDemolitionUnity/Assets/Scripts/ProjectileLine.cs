/****
 * Created By: Jacob Sharp
 * Date Created: Feb 16, 2022
 * 
 * Last Edited By: Jacob Sharp
 * Date Last Edited: Feb 17, 2022
 * 
 * Description: Create a trail behind projectiles
 ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    public static ProjectileLine S; // singleton

    [Header("SET IN INSPECTOR")]
    public float minDist = 0.1f;

    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    private void Awake()
    {
        S = this; // sets the singleton

        line = GetComponent<LineRenderer>(); // get LineRenderer reference and disable it
        line.enabled = false;

        points = new List<Vector3>(); // instantiate list
    }

    public GameObject poi // get/set point of interest
    {
        get { return (_poi); }
        set
        {
            _poi = value;
            if (_poi != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    public void Clear() // reset trail and point of interest
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }

    public void AddPoint() // add points to the trail
    {
        Vector3 pt = _poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist) return; // don't add point if it is too close to previously recorded points
        if (points.Count == 0) // if this is from the launch point
        {
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS;
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled = true;
        }
        else // add points normally
        {
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }

    public Vector3 lastPoint // get the most recent point
    {
        get
        {
            if (points == null) return (Vector3.zero);
            return (points[points.Count - 1]);
        }
    }

    private void FixedUpdate()
    {
        if (poi == null) // attempt to set new point of interest if one doesn't exist
        {
            if (FollowCam.target != null) 
            {
                if (FollowCam.target.tag == "Projectile") poi = FollowCam.target;
                else return;
            }
            else return;
        }

        AddPoint(); // add a new point

        if (FollowCam.target == null) poi = null; // reset point of interest if the camera has no follow target
    }

    
}
