using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace Assets.Scripts.User_Interface
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("Sound effects")]
        [SerializeField] private AudioClip onButtonSound;

        private AudioSource m_audioSource;

        private void Awake()
        {
            m_audioSource = FindFirstObjectByType<AudioSource>();
        }

        public void OnClick_Resume()
        {
            MenuManager.ChangeMenuState(Menu.PAUSE_MENU, MenuState.CLOSE);

            GameplayStateManager.Instance.SetState(GameplayState.Gameplay);

            m_audioSource.clip = onButtonSound;
            m_audioSource.Play();
        }

        public void OnClick_MainMenu()
        {
            MenuManager.ChangeMenuState(Menu.MAIN_MENU, MenuState.OPEN, gameObject);

            m_audioSource.clip = onButtonSound;
            m_audioSource.Play();
        }
    }
}