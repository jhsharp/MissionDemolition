/****
 * Created By: Jacob Sharp
 * Date Created: Feb 14, 2022
 * 
 * Last Edited By: Jacob Sharp
 * Date Last Edited: Feb 14, 2022
 * 
 * Description: Create multiple clouds throughout the level
 ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("SET IN INSPECTOR")]
    public int numClouds = 40;
    public GameObject cloudPrefab;
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1, cloudScaleMax = 3;
    public float cloudSpeedMult = 0.5f;

    private GameObject[] cloudInstances;

    private void Awake()
    {
        cloudInstances = new GameObject[numClouds]; // make an array to hold each cloud instance

        GameObject anchor = GameObject.Find("CloudAnchor"); // find anchor for cloud instances

        GameObject cloud; // create clouds
        for (int i = 0; i < numClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab); // instantiate new cloud

            Vector3 cPos = Vector3.zero; // randomly assign cloud position
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);

            float scaleU = Random.value; // randomly assign cloud scale
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU); // move smaller clouds closer to the ground and further away
            cPos.z = 100 - 90 * scaleU;

            cloud.transform.position = cPos; // apply position and scale to cloud transform
            cloud.transform.localScale = Vector3.one * scaleVal;

            cloud.transform.SetParent(anchor.transform); // set parent to cloud anchor

            cloudInstances[i] = cloud; // add cloud to instance list
        }
    }

    private void Update()
    {
        foreach (GameObject cloud in cloudInstances) // move clouds every frame
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            if (cPos.x <= cloudPosMin.x) // loop clouds from left to right when they hit the bounds
            {
                cPos.x = cloudPosMax.x;
            }
            cloud.transform.position = cPos;
        }
    }
}
