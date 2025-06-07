using TMPro;
using UnityEngine;

public class TowerLimitManager : MonoBehaviour
{
    public static TowerLimitManager Instance;

    public int maxTowersAllowed = 6;
    private int currentTowersPlaced = 0;

    public TextMeshProUGUI towerCountText; 

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateTowerUI(); // Initialize display
    }

    public bool CanPlaceTower()
    {
        return currentTowersPlaced < maxTowersAllowed;
    }

    public void RegisterTowerPlacement()
    {
        currentTowersPlaced++;
        UpdateTowerUI();
    }

    public void ResetTowerCount()
    {
        currentTowersPlaced = 0;
        UpdateTowerUI();
    }

    public void SetMaxTowersAllowed(int value)
    {
        maxTowersAllowed = value;
        UpdateTowerUI();
    }

    private void UpdateTowerUI()
    {
        if (towerCountText != null)
        {
            towerCountText.text = $"Towers: {currentTowersPlaced} / {maxTowersAllowed}";
        }
    }
}
