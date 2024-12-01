using System.Collections;
using UnityEngine;

namespace Assets.Scripts.User_Interface
{
    public class CreditsMenu : MonoBehaviour
    {
        [Header("Sound effects")]
        [SerializeField] private AudioClip onButtonSound;

        private AudioSource m_audioSource;

        private void Awake()
        {
            m_audioSource = FindFirstObjectByType<AudioSource>();
        }

        public void OnClick_Back()
        {
            MenuManager.ChangeMenuState(Menu.MAIN_MENU, MenuState.OPEN, gameObject);

            m_audioSource.clip = onButtonSound;
            m_audioSource.Play();
        }
    }
}