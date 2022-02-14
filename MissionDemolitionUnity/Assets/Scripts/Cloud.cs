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
            spTrans.localPosition = offset;

            Vector3 scale = Vector3.zero; // randomly set scale
            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y);
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);

            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x); // adjust y scale based on x distance from the center
            scale.y = Mathf.Max(scale.y, minScaleY);

            spTrans.localScale = scale; // assign scale
        }
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space)) // Reset cloud when space is pressed
        {
            Restart();
        }*/
    }

    private void Restart()
    {
        foreach (GameObject sp in spheres) // Destroy current cloud
        {
            Destroy(sp);
        }
        Start(); // Create new cloud
    }
}
