using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerPlacementManager : MonoBehaviour
{
    public static TowerPlacementManager Instance;

    public float cooldownTime = 3f;
    private bool isCooldown = false;
    private float cooldownTimer = 0f;

    [Header("UI Elements")]
    public Image cooldownFillImage;
    public TextMeshProUGUI limitReachedText;

    private Coroutine limitMessageRoutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (limitReachedText != null)
            limitReachedText.gameObject.SetActive(false); // Start hidden
    }

    void Update()
    {
        // Tower limit full
        if (!TowerLimitManager.Instance.CanPlaceTower())
        {
            if (cooldownFillImage != null)
            {
                cooldownFillImage.fillAmount = 1f;
                cooldownFillImage.color = Color.red;
            }

            if (limitMessageRoutine == null)
                limitMessageRoutine = StartCoroutine(ShowLimitReachedMessage());

            return;
        }

        // Cooldown logic (optional bar fill)
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownFillImage != null)
            {
                float fill = 1 - (cooldownTimer / cooldownTime);
                cooldownFillImage.fillAmount = fill;
                cooldownFillImage.color = Color.green;
            }

            if (cooldownTimer <= 0f)
                isCooldown = false;
        }
        else
        {
            if (cooldownFillImage != null)
            {
                cooldownFillImage.fillAmount = 1f;
                cooldownFillImage.color = Color.green;
            }
        }
      
    }

    public bool CanPlaceTower()
    {
        return !isCooldown && TowerLimitManager.Instance.CanPlaceTower();
    }

    public void PlaceTower(Transform location, GameObject towerPrefab)
    {
        GameObject tower = TowerPooler.Instance.SpawnFromPool(towerPrefab.tag, location.position, Quaternion.identity, location);
        TowerLimitManager.Instance.RegisterTowerPlacement();
        StartCooldown();
    }

    private void StartCooldown()
    {
        isCooldown = true;
        cooldownTimer = cooldownTime;

        if (cooldownFillImage != null)
            cooldownFillImage.fillAmount = 0f;
    }

    //  Show limit message for 2 seconds only
    private IEnumerator ShowLimitReachedMessage()
    {
        if (limitReachedText != null)
        {
            limitReachedText.text = "Tower Limit Reached";
            limitReachedText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        if (limitReachedText != null)
            limitReachedText.gameObject.SetActive(false);

        limitMessageRoutine = null;
    }
}
