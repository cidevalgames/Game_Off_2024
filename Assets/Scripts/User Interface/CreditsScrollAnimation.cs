using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.User_Interface
{
    public class CreditsScrollAnimation : MonoBehaviour
    {

        [SerializeField] private float scrollSpeed = 1f;

        private ScrollRect m_scrollRect;

        private void Awake()
        {
            m_scrollRect = GetComponent<ScrollRect>();
        }

        private void OnEnable()
        {
            m_scrollRect.verticalNormalizedPosition = .9999f;
            m_scrollRect.verticalNormalizedPosition = Mathf.Clamp(m_scrollRect.verticalNormalizedPosition, 0.0001f, 0.9999f);
        }

        private void Update()
        {
            m_scrollRect.velocity = Vector2.up * scrollSpeed;
        }
    }
}