using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    [SerializeField] private List<EnemyController> enemies;

    [SerializeField] private Vector3 playerPosition;

    private void Start()
    {
        GameManager.Instance.EnemiesTurn += OnEnemiesTurn;
        GameManager.Instance.PassTurn += OnPassTurn;
        GameManager.Instance.Win += OnWin;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            InitiateFight();
        }
    }

    private void InitiateFight()
    {
        GameManager.Instance.InitiateFight(new Vector3(0f, 0f, 30f));
    }

    private void OnEnemiesTurn()
    {
        enemies.RemoveAll(enemy => enemy == null);

        if(enemies.Count == 0)
        {
            Debug.Log("WIN");
            GameManager.Instance.OnWin();
            return;
        }

        foreach (var enemy in enemies)
        {
            enemy.EnemyStateMachine.ChangeState(enemy.EnemyTurnState);
        }

        var attackingEnemy = enemies[Random.Range(0, enemies.Count)];

        attackingEnemy.EnemyStateMachine.ChangeState(attackingEnemy.EnemyAttackState);
    }

    private void OnPassTurn()
    {
        enemies.RemoveAll(enemy => enemy == null);

        if (enemies.Count == 0)
        {
            Debug.Log("WIN");
            GameManager.Instance.OnWin();
            return;
        }

        foreach (var enemy in enemies)
        {
            enemy.EnemyStateMachine.ChangeState(enemy.EnemyWaitToAttackState);
        }
    }

    private void OnWin()
    {
        GameManager.Instance.EnemiesTurn -= OnEnemiesTurn;
        GameManager.Instance.PassTurn -= OnPassTurn;
        GameManager.Instance.Win -= OnWin;
        Destroy(gameObject);
    }
}
