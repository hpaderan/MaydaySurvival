using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float camHeight;
    public float camDepth;
    public float foo;

    void Update()
    {
        transform.LookAt(player.transform);

        Vector3 endPosition = player.transform.position + new Vector3(0,camHeight,-camDepth);
        //transform.position = endPosition;
        transform.position = Vector3.LerpUnclamped(transform.position, endPosition, foo);
    }
}
