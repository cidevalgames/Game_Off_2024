using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private KeyCode pauseKeyCode;

    private GameplayState _previousGameplayState;

    private void Update()
    {
        if (Input.GetKeyDown(pauseKeyCode))
        {
            GameplayState currentGameplayState = GameplayStateManager.Instance.CurrentGameplayState;

            if (currentGameplayState == GameplayState.Menu) return;

            GameplayState newGameplayState = GameplayState.Gameplay;

            if (currentGameplayState == GameplayState.Gameplay || currentGameplayState == GameplayState.Tutorial)
            {
                _previousGameplayState = currentGameplayState;

                newGameplayState = GameplayState.Paused;
            }
            else if (currentGameplayState == GameplayState.Paused)
            {
                newGameplayState = _previousGameplayState;
            }

            GameplayStateManager.Instance.SetState(newGameplayState);
        }
    }
}