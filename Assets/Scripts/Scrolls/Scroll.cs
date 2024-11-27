using UnityEngine;

public class Scroll : DevObject
{
    [SerializeField]
    [Header("Destination pour fin conspiration")]
    private EScrollDestination cDestination;
    
    [SerializeField]
    [Header("Destination pour fin héroïque")]
    private EScrollDestination hDestination;
    
    [SerializeField]
    [Header("Texte du parchemin"),TextArea(30,100)]
    private string text;
}