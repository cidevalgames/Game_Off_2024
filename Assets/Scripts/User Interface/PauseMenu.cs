using System.Collections;
using UnityEngine;

namespace Assets.Scripts.User_Interface
{
    public class PauseMenu : MonoBehaviour
    {
        public void OnClick_Resume()
        {
            MenuManager.ChangeMenuState(Menu.PAUSE_MENU, MenuState.CLOSE);

            GameplayStateManager.Instance.SetState(GameplayState.Gameplay);
        }

        public void OnClick_MainMenu()
        {
            MenuManager.ChangeMenuState(Menu.MAIN_MENU, MenuState.OPEN, gameObject);
        }
    }
}