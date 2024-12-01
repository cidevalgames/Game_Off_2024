using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.User_Interface
{
    [CreateAssetMenu(fileName = "Dialog_Tuto_", menuName = "My Assets/New tuto dialog", order = 0)]
    public class DialogTutoSO : ScriptableObject
    {
        [TextArea(5, 10)] public List<string> texts = new();
    }
}