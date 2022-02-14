/****
 * Created By: Jacob Sharp
 * Date Created: Feb 14, 2022
 * 
 * Last Edited By: Jacob Sharp
 * Date Last Edited: Feb 14, 2022
 * 
 * Description: Create a randomly generated cloud
 ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [Header("SET IN INSPECTOR")]
    public GameObject cloudSphere; // prefabs that make up the cloud
    public int minSpheres = 6, maxSpheres = 10;
    public Vector2 sphereScaleRangeX = new Vector2(4, 8);
    public Vector2 sphereScaleRangeY = new Vector2(3, 4);
    public Vector2 sphereScaleRangeZ = new Vector2(2, 4);
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1);
    public float minScaleY = 2f;

    private List<GameObject> spheres = new List<GameObject>();

    private void Start()
    {
        int num = Random.Range(minSpheres, maxSpheres); // randomly select number of cloud spheres
        for (int i = 0; i < num; i++)
        {
            GameObject sp = Instantiate<GameObject>(cloudSphere); // create new cloud
            spheres.Add(sp);

            Transform spTrans = sp.transform; // set parent
            spTrans.SetParent(transform);

            Vector3 offset = Random.insideUnitSphere; // randomly assign a position
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;
        }
    }
}
