using Assets.Scripts.User_Interface;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

[Serializable]
public class TutoDialog
{
    public DialogTutoSO m_dialogTutoSO;
    public List<UnityEvent> onTrigger;
    public UnityEvent onDialogEnd;
}
