using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    [SerializeField] private EnemyController[] enemies;

    [SerializeField] private Vector3 playerPosition;

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

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].ChangeState(Enums.State.Fight);
        }
    }
}
