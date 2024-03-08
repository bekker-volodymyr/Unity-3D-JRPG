using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    private Collider _collider;
    [SerializeField] private GameObject[] enemies;
    void Start()
    {
        _collider = GetComponent<Collider>();
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
        
    }
}
