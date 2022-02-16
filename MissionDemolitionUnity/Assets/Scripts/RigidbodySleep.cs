/****
 * Created By: Jacob Sharp
 * Date Created: Feb 16, 2022
 * 
 * Last Edited By: Jacob Sharp
 * Date Last Edited: Feb 16, 2022
 * 
 * Description: Put rigidbody to sleep for one frame
 ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class RigidbodySleep : MonoBehaviour
{
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>(); // make rigidbody sleep
        if (rb != null) rb.Sleep();
    }
}
