using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float CameraSpeed;
    public float MoveSpeed,ZoomSpeed;
    public float currentx;
    public float currenty;
    public bool following;
    public Vector3 velocity = Vector3.zero;
    private Camera cam;
    public float CamSize;

    private void Start()
    {
        cam = GetComponent<Camera>();
        CamSize = cam.orthographicSize;
    }
    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentx, currenty, transform.position.z),ref velocity,CameraSpeed/* *Time.deltaTime */);
        cam.orthographicSize = CamSize;
        InputsCamera();
    }

    public void MoveCamera(float x, float y) 
    { 
        currentx = x;
        currenty = y;
    }
    public void InputsCamera() 
    {
        MoveCamera(currentx+ MoveSpeed*Time.deltaTime*Input.GetAxis("Horizontal"), currenty + MoveSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
        CamSize += ZoomSpeed * Time.deltaTime*Input.mouseScrollDelta.y;
    }
}
