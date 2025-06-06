using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    public string waveName;
    public List<EnemySpawnInfo> enemies;
}

[System.Serializable]
public class EnemySpawnInfo
{
    public EnemyType type;
    public int count;
}