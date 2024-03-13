using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    private Collider _collider;
    [SerializeField] private EnemyController[] enemies;

    [SerializeField] private Vector3 playerPosition;

    private PlayerMovement player;
    void Start()
    {
        _collider = GetComponent<Collider>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
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
        player.ChangeState(Enums.State.Fight, playerPosition);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].ChangeState(Enums.State.Fight);
        }
    }
}
