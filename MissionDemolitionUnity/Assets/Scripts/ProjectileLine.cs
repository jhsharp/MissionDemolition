/****
 * Created By: Jacob Sharp
 * Date Created: Feb 16, 2022
 * 
 * Last Edited By: Jacob Sharp
 * Date Last Edited: Feb 16, 2022
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
    public float minDistance = 0.1f;

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

    public GameObject poi
    {
        get { return (_poi); }
        set
        {
            _poi = value;
            if (_poi != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoints();
            }
        }
    }
}
