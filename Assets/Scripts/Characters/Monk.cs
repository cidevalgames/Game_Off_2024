using System.Collections.Generic;
using Assets.Scripts.User_Interface;
using UnityEngine;

public class Monk : DevObject
{
    [SerializeField] private List<DialogSO> dialogsList;

    [Header("Nombre maximum d'apparitions du moine par jour :")]
    [SerializeField]
    private int maxAppearancePerDay;

    [Header("Probabilité d'apparition du moine à chaque heure, entre 0 et 1")]
    [Tooltip("[0.0 ; 1.0] - 0.5 équivaut à une probabilité de 50% d'appartion.")]
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float appearanceProbability;
    
    private int _appearanceCountThisDay;
    
    public void QueueDialog(int dialogIndex, int remainingHearts)
    {
        List<string> dialog = new();

        switch (remainingHearts)
        {
            case 2:
                dialog = dialogsList[dialogIndex].texts2Hearts;
                break;
            case 1:
                dialog = dialogsList[dialogIndex].texts1Heart;
                break;
            case 0:
                dialog = dialogsList[dialogsList.Count - 1].textDeath;
                break;
        }

        GameState().dialogTextPanel.textQueue = new List<string>(dialog);
        GameState().dialogTextPanel.Dialog(0);
    }

    [ContextMenu("Trigger text 1")]
    public void TriggerText1()
    {
        QueueDialog(0, 1);
    }

    public void resetAppearanceCount()
    {
        _appearanceCountThisDay = 0;
    }

    public void Appear()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            Debug.LogWarning("Monk sprite renderer is null");
        }
        _appearanceCountThisDay++;
        Debug.Log("Apparition du moine !");


        // TODO Utiliser QueueDialog() pour afficher le dialogue quand le moine apparaît.
    }
    
    public void Disappear()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            Debug.LogWarning("Monk sprite renderer is null");
        }
        
        GameState().periodManager.GetCurrentPeriod().NextHour();
    }

    private void Awake()
    {
        GameState().monk = this;
    }

    public void AppearanceRoll()
    {
        if (Random.value < appearanceProbability && _appearanceCountThisDay < maxAppearancePerDay)
        {
            Appear();
            return;
        }
        
        GameState().periodManager.GetCurrentPeriod().NextHour();
    }
}
