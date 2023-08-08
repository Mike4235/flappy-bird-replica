using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    // Update is called once per frame
    void LateUpdate()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 tempPos = transform.position;
            tempPos.x = player.transform.position.x;
            transform.position = tempPos;
        }

    }
}
