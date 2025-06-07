using UnityEngine;
using UnityEngine.UI;

public class TowerSelectionUI : MonoBehaviour
{
    public static TowerSelectionUI Instance;

    public GameObject panel;  // UI panel with tower buttons
    private TowerPlacer currentPlacer;

    public GameObject fireTowerPrefab;
    public GameObject freezeTowerPrefab;
    public GameObject poisonTowerPrefab;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void OpenSelectionMenu(TowerPlacer placer)
    {
        currentPlacer = placer;
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        panel.SetActive(false);
        currentPlacer = null;
        Time.timeScale = 1f;
    }

    public void SelectFireTower()
    {
        PlaceSelectedTower(fireTowerPrefab);
    }

    public void SelectFreezeTower()
    {
        PlaceSelectedTower(freezeTowerPrefab);
    }

    public void SelectPoisonTower()
    {
        PlaceSelectedTower(poisonTowerPrefab);
    }

    private void PlaceSelectedTower(GameObject prefab)
    {
        if (currentPlacer != null)
        {
            currentPlacer.PlaceSelectedTower(prefab);
            CloseMenu();
        }
    }
}
