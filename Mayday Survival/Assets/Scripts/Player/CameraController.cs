using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float camHeight;
    public float camDepth;

    void Update()
    {
        Vector3 endPosition = player.transform.position + new Vector3(0,camHeight,-camDepth);
        Vector3 lerpPosition = Vector3.Lerp(transform.position,endPosition,0.1f);
        transform.position = lerpPosition;
        //  transform.LookAt(player.transform.position);
    }
}
