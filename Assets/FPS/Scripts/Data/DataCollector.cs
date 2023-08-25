using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCollector : MonoBehaviour
{
    public static int DeathCount = 0;
    public static int KilledEnemy = 0;
    public static void SaveDeathCount(int deathCount)
    {
        DeathCount = deathCount;
    }

    public static void SaveEnemyCount(int enemyCount)
    {
        KilledEnemy = enemyCount;
    }

}
