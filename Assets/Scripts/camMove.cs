using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float translationSpeed = 8f; 
   

    private float cameraYaw = 0f; 
    private float cameraPitch = 0f;

    public float turnSpeed = 120f;
    public float zoomIntensity = 15f;

    void Update()
    {
       
        HandleMovement();

        
        if (Input.GetMouseButton(1)) 
        {
            HandleRotation();
        }

        
        HandleZoom();
    }

    void HandleZoom()
    {

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        Vector3 zoomDirection = transform.forward * scrollInput * zoomIntensity * Time.deltaTime;
        transform.position += zoomDirection;
    }


    void HandleRotation()
    {
       
        float mouseDeltaX = Input.GetAxis("Mouse X"); 
        float mouseDeltaY = Input.GetAxis("Mouse Y"); 

       
        cameraYaw += mouseDeltaX * turnSpeed * Time.deltaTime;
        cameraPitch -= mouseDeltaY * turnSpeed * Time.deltaTime;

        
        cameraPitch = Mathf.Clamp(cameraPitch, -75f, 75f);

       
        transform.rotation = Quaternion.Euler(cameraPitch, cameraYaw, 0f);
    }

    void HandleMovement()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 moveVector = new Vector3(horizontalInput, 0f, verticalInput);
        transform.Translate(moveVector * translationSpeed * Time.deltaTime, Space.Self);
    }
}