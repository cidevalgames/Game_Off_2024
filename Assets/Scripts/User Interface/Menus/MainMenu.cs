using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Scripts.User_Interface
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField, Tooltip("Fade in duration in seconds")] private float fadeInDuration = 1f;

        [Header("Sound effects")]
        [SerializeField] private AudioClip onStartSound;
        [SerializeField] private AudioClip onButtonSound;
         
        private VideoPlayer m_videoPlayer;
        private AudioSource m_audioSource;

        [ContextMenu("Skip transition")]
        public void SkipTransition() => m_videoPlayer.time = m_videoPlayer.length;

        private void Awake()
        {
            GetComponentInChildren<CanvasGroup>().alpha = 0;

            m_videoPlayer = FindFirstObjectByType<VideoPlayer>();
            m_videoPlayer.loopPointReached += OnTransitionEnd;

            m_audioSource = FindFirstObjectByType<AudioSource>();
        }

        private void OnTransitionEnd(VideoPlayer videoPlayer)
        {
            GetComponentInChildren<CanvasGroup>().DOFade(1, fadeInDuration).OnComplete(EnableButtonsInteraction);
        }

        private void EnableButtonsInteraction()
        {
            foreach (Button button in GetComponentsInChildren<Button>())
            {
                button.interactable = true;
            }
        }

        public void OnClick_Start()
        {
            MenuManager.ChangeMenuState(Menu.MAIN_MENU, MenuState.CLOSE);

            m_audioSource.clip = onStartSound;
            m_audioSource.Play();

            GameplayStateManager.Instance.SetState(GameplayState.Tutorial);

            FindFirstObjectByType<TutoDialogManager>().QueueDialog(0);
        }

        public void OnClick_Credits()
        {
            MenuManager.ChangeMenuState(Menu.CREDITS_MENU, MenuState.OPEN, gameObject);

            m_audioSource.clip = onButtonSound;
            m_audioSource.Play();
        }

        public void OnClick_Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}