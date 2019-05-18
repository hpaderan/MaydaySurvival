using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    public float currentZoom = 10f;
    public float cameraSpeed = 100f;
    public float pitch = 2f;
    public float Zoomspeed = 4f, minZoom = 5f, maxZoom = 15f;
    private float cameraInput = 0f;
    // LateUpdate is Update method except it is called right after Update
    private void Update()
    {
        //allow your mouse to scroll for zoom in or zoom out
        currentZoom -= Input.GetAxis("Mouse ScrollWheel")* Zoomspeed;
        //set boundary for the zoom
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        //set the left right zoom
        cameraInput -= Input.GetAxis("Horizontal")*cameraSpeed*Time.deltaTime;
    }
    void LateUpdate()
    {
        //the position of the camera after moving it
        transform.position = target.position - offset * currentZoom;
        //the focus point 
        transform.LookAt(target.position + Vector3.up * pitch);
        //rotate the camera
        transform.RotateAround(target.position, Vector3.up, cameraInput);
    }
}
