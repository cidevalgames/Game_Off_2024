using UnityEngine;

/// <summary>
/// Utiliser cette classe comme une classe globale permettant de stocker des informations qui doivent être accessibles
/// depuis tous les objets du jeu.
/// </summary>
public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [HideInInspector]
    public PeriodManager periodManager;
    
    [HideInInspector]
    public Monk monk;
    
    [HideInInspector]
    public DialogTextPanel dialogTextPanel;
    
    [HideInInspector]
    public VillageRack villageRack;
    
    [HideInInspector]
    public VilleRack villeRack;

    [Header("Périodes d'une journée")] [SerializeField] [Min(0)]
    public int daysNumber;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning($"GameState already exists on {gameObject.name}. Object has been marked for destruction");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
