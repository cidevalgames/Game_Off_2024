using System.Collections.Generic;
using UnityEngine;

public class Monk : DevObject
{
    [Header("Nombre maximum d'apparitions du moine par jour :")]
    [SerializeField]
    private int maxAppearancePerDay;

    [Header("Probabilité d'apparition du moine à chaque heure, entre 0 et 1")]
    [Tooltip("[0.0 ; 1.0] - 0.5 équivaut à une probabilité de 50% d'appartion.")]
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float appearanceProbability;
    
    private int _appearanceCountThisDay;
    
    public void QueueDialog(List<string> dialog)
    {
        GameState().dialogTextPanel.textQueue = dialog;
        GameState().dialogTextPanel.Dialog(0);
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
