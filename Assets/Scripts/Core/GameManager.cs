using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CinemachineFreeLook primaryCamera;
    public CinemachineVirtualCamera fightingCamera;

    public Player player;
    public Transform playerFightPosition;

    public bool isPaused;

    public GameObject attackTarget;

    public Action FightInitiationEvent;
    public Action AttackEnded;
    public Action EnemyAttack;
    public Action EnemiesTurn;
    public Action PassTurn;
    public Action Win;

    public bool isPlayerTurn = true;

    [SerializeField] private GameObject WinPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void InitiateFight(Vector3 pos)
    {
        primaryCamera.gameObject.SetActive(false);
        fightingCamera.gameObject.SetActive(true);

        player.StateMachine.ChangeState(player.MoveToFightPositionState);

        FightInitiationEvent?.Invoke();

        Cursor.visible = true;
    }

    public void Attack(GameObject ghost)
    {
        attackTarget = ghost;
        player.StateMachine.ChangeState(player.MoveToTargetState);
    }

    public void OnWin()
    {
        fightingCamera.gameObject.SetActive(false);
        primaryCamera.gameObject.SetActive(true);

        player.StateMachine.ChangeState(player.IdleState);

        WinPanel.SetActive(true);

        Win?.Invoke();
    }
}
