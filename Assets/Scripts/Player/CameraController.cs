using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject cameraAnchor;
    private Vector3 cameraAngles;
    private Vector3 cameraOffset;
    private Vector3 initialAngles;
    private Vector3 initialOffset;

    private Enums.State currentState;

    Vector3 fightPosition = new Vector3(0f, 3.3f, 9.5f);
    Vector3 fightRotation = new Vector3(15f, -50f, 0f);
    Vector3 fightCenter = new Vector3(0f, 0f, 20f);

    #region Lerp Variables

    // Init on lerp start
    private float lerpStartTime;
    private Vector3 lerpStartPos;
    private float lerpLength;

    private float lerpSpeed = 1.0f;

    #endregion

    void Start()
    {
        initialAngles = cameraAngles = transform.eulerAngles;
        initialOffset = cameraOffset = transform.position - cameraAnchor.transform.position;

        currentState = Enums.State.Idle;
    }

    void Update()
    {
        switch (currentState)
        {
            case Enums.State.Idle:
                IdleUpdate(); 
                break;
            case Enums.State.Fight:
                //FightUpdate(); 
                break;
            default: break;
        }
    }

    private void LateUpdate()
    {
        switch (currentState)
        {
            case Enums.State.Idle:
                IdleLateUpdate(); break;
            case Enums.State.Fight:
                FightLateUpdate(); break;
            default: break;
        }
    }

    public void ChangeState(Enums.State newState)
    {
        currentState = newState;

        if (newState == Enums.State.Fight)
        {
            Debug.Log("Fight state init for camera");

            lerpStartTime = Time.time;
            lerpStartPos = transform.position;
            lerpLength = Vector3.Distance(lerpStartPos, fightPosition);

            
        }
    }

    private void IdleUpdate()
    {
        cameraAngles.y += Input.GetAxis("Mouse X");
        cameraAngles.x -= Input.GetAxis("Mouse Y");


        if (cameraAngles.x < 0)
        {
            cameraAngles.x = 0;
        }
        else if (cameraAngles.x > 30)
        {
            cameraAngles.x = 30;
        }
    }

    private void IdleLateUpdate()
    {
        transform.position = cameraAnchor.transform.position + Quaternion.Euler(0, cameraAngles.y - initialAngles.y, 0) * cameraOffset;
        transform.eulerAngles = cameraAngles;
    }

    private void FightUpdate()
    {
        float distCovered = (Time.time - lerpStartTime) * lerpSpeed;

        float fractionOfJourney = distCovered / lerpLength;

        transform.position = Vector3.Lerp(lerpStartPos, fightPosition, fractionOfJourney);
    }

    private void FightLateUpdate()
    {
        float distCovered = (Time.time - lerpStartTime) * lerpSpeed;

        float fractionOfJourney = distCovered / lerpLength;

        transform.Rotate(Vector3.up, 5f);
        transform.position = Vector3.Lerp(lerpStartPos, fightPosition, fractionOfJourney);
        
    }
}
