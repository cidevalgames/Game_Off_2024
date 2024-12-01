using Assets.Scripts.User_Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutoDialogManager : DevObject
{
    public List<TutoDialog> TutoDialogList;

    private void OnEnable()
    {
        StartCoroutine(OnLateStart());
    }

    private IEnumerator OnLateStart()
    {
        yield return new WaitForEndOfFrame();

        QueueDialog(0);
    }

    public void QueueDialog(int dialogIndex)
    {
        List<string> dialog = TutoDialogList[dialogIndex].m_dialogTutoSO.texts;

        GameState().dialogTextPanel.textQueue = new List<string>(dialog);
        GameState().dialogTextPanel.eventQueue = new List<UnityEvent>(TutoDialogList[dialogIndex].onTrigger);
        GameState().dialogTextPanel.Dialog(0, TutoDialogList[dialogIndex].onDialogEnd);
    }

    [ContextMenu("Set tuto dialog list")]
    private void Set()
    {
        foreach (var tutoDialog in TutoDialogList)
        {
            tutoDialog.onTrigger.Clear();

            for (int i = 0; i < tutoDialog.m_dialogTutoSO.texts.Count; i++)
            {
                tutoDialog.onTrigger.Add(new UnityEvent());
            }
        }
    }
}
