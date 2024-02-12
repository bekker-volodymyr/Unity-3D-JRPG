using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject cameraAnchor;
    private Vector3 cameraAngles;
    private Vector3 cameraOffset; 
    private Vector3 initialAngles; 
    private Vector3 initialOffset;

    void Start()
    {
        initialAngles = cameraAngles = transform.eulerAngles;
        initialOffset = cameraOffset = transform.position - cameraAnchor.transform.position;
        
    }

    void Update()
    {
        cameraAngles.y += Input.GetAxis("Mouse X");
        Debug.Log(cameraAngles.x);
        cameraAngles.x -= Input.GetAxis("Mouse Y");

        if (cameraOffset == Vector3.zero)
        {
            if (cameraAngles.x < -80)
            {
                cameraAngles.x = -80;
            }
            else if (cameraAngles.x > 85)
            {
                cameraAngles.x = 85;
            }
        }
        else
        {
            if (cameraAngles.x < -20)
            {
                cameraAngles.x = -20;
            }
            else if (cameraAngles.x > 20)
            {
                cameraAngles.x = 20;
            }
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            cameraOffset = (cameraOffset == Vector3.zero) ? initialOffset : Vector3.zero;
        }
    }

    private void LateUpdate()
    {
        transform.position = cameraAnchor.transform.position + Quaternion.Euler(0, cameraAngles.y - initialAngles.y, 0) * cameraOffset;
        transform.eulerAngles = cameraAngles;
    }
}
