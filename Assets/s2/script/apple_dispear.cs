using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apple_dispear : MonoBehaviour
{

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 0.6f))
        {
            Destroy(gameObject,5.0f);
        }
    }
}