using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public EnemyPooler enemyPooler;

    public TextMeshProUGUI waveText;
    public EnemySpawner enemySpawner;

    public const int totalWaves = 3;

    private int currentWave = 0;
    private bool isSpawning = false;

    public GameCompleteUI gameCompleteUI;

    public TextMeshProUGUI waveInfoText;

  
    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

    private void Update()
    {
        // Check if all enemies are dead and we're not already spawning
        if (!isSpawning && enemyPooler.GetActiveEnemyCount() == 0)
        {
            if (currentWave < totalWaves)
            {
                ClearAllTowers();
                StartCoroutine(StartNextWave());

            }
        }
        else
            gameCompleteUI.ShowGameComplete(); // Show game complete UI if all waves are done

    }

    IEnumerator StartNextWave()
    {
        isSpawning = true;
        currentWave++;
        
        int maxTowers = GetMaxTowersForWave(currentWave);
        TowerLimitManager.Instance.SetMaxTowersAllowed(maxTowers);
       
        // Get enemy and tower count
        int enemyCount = GetEnemyCountForWave(currentWave);
       

        // Show wave start text
        waveText.text = $"Wave {currentWave} Starting!";
        waveInfoText.text = $"Enemies: {enemyCount}";

        waveText.enabled = true; 
        waveInfoText.enabled = true;
        
        // Wait for a few seconds so player can read the info
        yield return new WaitForSeconds(2f);

        // Hide wave text and info text
        waveText.enabled = false;
        waveInfoText.enabled = false; 


        // Spawn enemies for this wave
        yield return StartCoroutine(SpawnWave(currentWave));

        isSpawning = false;
    }


    IEnumerator SpawnWave(int wave)
    {
        switch (wave)
        {
            case 1:
                yield return StartCoroutine(SpawnEnemies(EnemyType.Slow, 15));
                break;
            case 2:
                yield return StartCoroutine(SpawnEnemies(EnemyType.Normal, 10));
                yield return StartCoroutine(SpawnEnemies(EnemyType.Fast, 5));
                break;
            case 3:
                yield return StartCoroutine(SpawnEnemies(EnemyType.Tank, 5));
                yield return StartCoroutine(ShowBossMessage());
                yield return StartCoroutine(SpawnEnemies(EnemyType.Boss, 1));
                break;
        }

    }

    IEnumerator SpawnEnemies(EnemyType type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            enemySpawner.SpawnEnemy(type);
            yield return new WaitForSeconds(Random.Range(1.5f,2f)); // delay between spawns
        }
    }
    void ClearAllTowers()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        foreach (GameObject tower in towers)
        {
            tower.GetComponent<Tower>().DestroyTower();  
        }
    }
    IEnumerator ShowBossMessage()
    {
        waveText.text = "Boss Incoming!";
        waveText.enabled = true;
        waveText.alpha = 1f;

        yield return new WaitForSeconds(2f);  // Boss warning duration

        waveText.enabled = false;
    }
    int GetEnemyCountForWave(int wave)
    {
        switch (wave)
        {
            case 1:
                return 15;
            case 2:
                return 10 + 5;
            case 3:
                return 5 + 1;
            default:
                return 0;
        }
    }
    int GetMaxTowersForWave(int wave)
    {
        switch (wave)
        {
            case 1: return 6;
            case 2: return 8;
            case 3: return 10;
            default: return 12;
        }
    }

}
