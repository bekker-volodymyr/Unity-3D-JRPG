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

    public PlayerStateMachine StateMachine { get; set; }
    public IdleState IdleState { get; set; }
    public MoveToFightPositionState MoveToFightPositionState { get; set; }
    public WaitToAttackTargetState WaitToAttackTargetState { get; set; }
    public MoveToTargetState MoveToTargetState { get; set; }
    public AttackState AttackState { get; set; }

    #endregion

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;

        StateMachine = new PlayerStateMachine();

        IdleState = new IdleState(StateMachine, this);
        MoveToFightPositionState = new MoveToFightPositionState(StateMachine, this);
        WaitToAttackTargetState = new WaitToAttackTargetState(StateMachine, this);
        MoveToTargetState = new MoveToTargetState(StateMachine, this);
        AttackState = new AttackState(StateMachine, this);

        StateMachine.Init(IdleState);

        Cursor.visible = false;
    }

    private void Update()
    {
        StateMachine.CurrentState.FrameUpdate();
    }

    public void SetAnimatorState(int state)
    {
        animator.SetInteger("State", state);
    }

    public void IdleMove(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDir.normalized * speed * Time.deltaTime);
    }


    public void OnAttackAnimationEnd()
    {
        GameManager.Instance.AttackEnded?.Invoke();
    }
}
