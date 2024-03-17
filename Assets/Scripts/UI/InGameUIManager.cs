using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    // [SerializeField] private GameObject playerMenu;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject pauseBtn;
    // [SerializeField] private GameObject playerMenuBtn;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        pauseMenu.SetActive(!GameManager.Instance.isPaused);
        GameManager.Instance.isPaused = !GameManager.Instance.isPaused;

        pauseBtn.SetActive(!GameManager.Instance.isPaused);

        Time.timeScale = GameManager.Instance.isPaused ? 0f : 1f;
    }

    public void TogglePlayerMenu()
    {
        //playerMenu.SetActive(!GameState.isInPlayerMenu);
        //GameState.isInPlayerMenu = !GameState.isInPlayerMenu;

        //pauseBtn.SetActive(!GameState.isInPlayerMenu);
        //playerMenuBtn.SetActive(!GameState.isInPlayerMenu);

        //Time.timeScale = GameState.isInPlayerMenu ? 0f : 1f;
    }

}
