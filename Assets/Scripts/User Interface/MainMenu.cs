using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;
using UnityEngine.UI;

namespace Assets.Scripts.User_Interface
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField, Tooltip("Fade in duration in seconds")] private float fadeInDuration = 1f;
         
        private VideoPlayer m_videoPlayer;

        [ContextMenu("Skip transition")]
        public void SkipTransition() => m_videoPlayer.time = m_videoPlayer.length;

        private void Awake()
        {
            GetComponentInChildren<CanvasGroup>().alpha = 0;

            m_videoPlayer = FindFirstObjectByType<VideoPlayer>();
            m_videoPlayer.loopPointReached += OnTransitionEnd;
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

            GameplayStateManager.Instance.SetState(GameplayState.Gameplay);
        }

        public void OnClick_Credits()
        {
            MenuManager.ChangeMenuState(Menu.CREDITS_MENU, MenuState.OPEN, gameObject);
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