using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    // Ye spot pe kya tower place hoga wo yahan se set hoga
 //   public Transform placementSpot;

    private void OnMouseDown()
    {
        if (transform.childCount == 0 && TowerPlacementManager.Instance.CanPlaceTower())
        {
            TowerSelectionUI.Instance.OpenSelectionMenu(this);
        }
    }

    // Ye method UI se call hoga
    public void PlaceSelectedTower(GameObject selectedTowerPrefab)
    {
        TowerPlacementManager.Instance.PlaceTower(transform, selectedTowerPrefab);
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
