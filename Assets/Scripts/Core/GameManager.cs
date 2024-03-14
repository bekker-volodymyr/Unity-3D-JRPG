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
    }
}
