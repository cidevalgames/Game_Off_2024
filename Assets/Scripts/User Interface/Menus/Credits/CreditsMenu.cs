using System.Collections;
using UnityEngine;

namespace Assets.Scripts.User_Interface
{
    [RequireComponent(typeof(AudioSource))]
    public class CreditsMenu : MonoBehaviour
    {
        [Header("Sound effects")]
        [SerializeField] private AudioClip onButtonSound;

        private AudioSource m_audioSource;

        public void OnClick_Back()
        {
            MenuManager.ChangeMenuState(Menu.MAIN_MENU, MenuState.OPEN, gameObject);

            m_audioSource.clip = onButtonSound;
            m_audioSource.Play();
        }
    }
}