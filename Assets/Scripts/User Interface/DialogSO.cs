using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.User_Interface
{
    [CreateAssetMenu(fileName = "Dialog_", menuName = "My Assets/New dialog", order = 0)]
    public class DialogSO : ScriptableObject
    {
        [Header("2 coeurs restants"), TextArea(5, 10)]
        public List<string> texts2Hearts = new();

        [Header("1 coeur restant"), TextArea(5, 10)]
        public List<string> texts1Heart = new();

        [Header("0 coeur restant"), TextArea(5, 10)]
        public List<string> textDeath = new();
    }
}