using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostNPC : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetInteger("State", 1);
        }
    }
}
