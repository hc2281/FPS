using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class LogRecorder : MonoBehaviour
{
    public static int KilledEnemy;
    public static Stack<int> enemyCountList = new Stack<int>();
    private string logFileName;
    private string logFilePath;

    public static void SaveEnemyCount(int enemyCount)
    {
        int LastCount = KilledEnemy;
        Debug.Log("Current Killed Enemy Count:" + KilledEnemy);
        if (enemyCount > LastCount)
        {
            if (enemyCountList.Count != 0)
            {
                enemyCountList.Pop();
            }
            enemyCountList.Push(enemyCount);
            KilledEnemy = enemyCount;
            Debug.Log("Current Killed Enemy Count:" + KilledEnemy);
        }
        else if (enemyCount <= LastCount)
        {
            enemyCountList.Push(enemyCount); 
            KilledEnemy = enemyCount;
        }
    }
                                                                            
    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string dateTimeString = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        logFileName = sceneName + $"_debug_log_{dateTimeString}.txt";
        string logsFolderPath = Path.Combine(Application.dataPath, "logs");
        if (!Directory.Exists(logsFolderPath))
        {
            Directory.CreateDirectory(logsFolderPath);
        }

        logFilePath = Path.Combine(logsFolderPath, logFileName);

        Application.logMessageReceived += LogMessageReceived;
    }

    private void LogMessageReceived(string condition, string stackTrace, LogType type)
    {
        string logEntry = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " [" + type.ToString() + "] " + condition + "\n" + stackTrace + "\n";
        File.AppendAllText(logFilePath, logEntry);
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= LogMessageReceived;
    }
}
