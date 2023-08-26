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
        if (enemyCount > LastCount)
        {
            KilledEnemy = enemyCount;
            Debug.Log("Current Killed Enemy Count:" + KilledEnemy);
        }
        else if (enemyCount <= LastCount)
        {
            enemyCountList.Push(LastCount);  // Push the highest count of the previous sequence
            KilledEnemy = enemyCount;        // Reset KilledEnemy for the new sequence
        }
    }
                                                                            
    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        logFileName = sceneName + "_debug_log.txt";
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
