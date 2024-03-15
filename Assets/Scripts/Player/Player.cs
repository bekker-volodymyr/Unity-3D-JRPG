using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Transform cameraTransform;

    private float turnSmoothVelocity;

    [SerializeField] private float speed = 6f;
    [SerializeField] private float turnSmoothTime = 0.1f;

    #region States Variables
    private Enums.State currentState;

    #endregion

    #region Lerp Variables

    // Init on lerp start
    private float lerpStartTime;
    private Vector3 lerpStartPos;
    private float lerpLength;

    private float lerpSpeed = 5.0f;

    #endregion

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;

        currentState = Enums.State.Idle;
    }

    private void Update()
    {
        switch (currentState)
        {
            case Enums.State.Idle:
                IdleUpdate();
                break;
            case Enums.State.Fight:
                FightUpdate(); 
                break;
            default: break;
        }
    }

    public void ChangeState(Enums.State newState)
    {
        switch (newState)
        {
            case Enums.State.Idle: break;
            case Enums.State.Fight:
                lerpStartTime = Time.time;
                lerpStartPos = transform.position;
                lerpLength = Vector3.Distance(lerpStartPos, GameManager.Instance.playerFightPosition.position);
                break;
            default: break;
        }
        currentState = newState;
    }

    private void IdleUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            animator.SetInteger("State", 1);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void FightUpdate()
    {
        float distCovered = (Time.time - lerpStartTime) * lerpSpeed;

        float fractionOfJourney = distCovered / lerpLength;

        if(fractionOfJourney >= 1f)
        {
            animator.SetInteger("State", 0);
        }
        else
        {
            transform.position = Vector3.Lerp(lerpStartPos, GameManager.Instance.playerFightPosition.position, fractionOfJourney);
        }
    }
}
