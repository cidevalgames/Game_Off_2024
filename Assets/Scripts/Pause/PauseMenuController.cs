using System;
using System.Collections;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    private void Awake()
    {
        GameplayStateManager.Instance.OnGameplayStateChanged += OnGameplayStateChanged;
    }

    private void OnDestroy()
    {
        GameplayStateManager.Instance.OnGameplayStateChanged -= OnGameplayStateChanged;
    }

    private void OnGameplayStateChanged(GameplayState newGameplayState)
    {
        if (newGameplayState == GameplayState.Paused)
        {
            MenuManager.ChangeMenuState(Menu.PAUSE_MENU, newGameplayState == GameplayState.Gameplay ? MenuState.CLOSE : MenuState.OPEN);
        }
    }
}