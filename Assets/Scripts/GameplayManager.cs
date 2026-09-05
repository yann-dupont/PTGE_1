using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    public int playerHealth = 3;
    public int playerMaxHealth = 5;
    public float playerShakra = 100;
    public float playerMaxShakra = 100;
    public float playerScore = 0;
    public float playerPreScore = 0;
    private HashSet<string> defeatedEnemies = new HashSet<string>();
    private bool tomatounlocked = false;
    private bool AllEnemiesKilled = false;
    private bool tutodone = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void MarkEnemyDefeated(string id)
    {
        defeatedEnemies.Add(id);
        if (defeatedEnemies.Count==2)
        {
            tomatounlocked=true;
            Scene currentScene = SceneManager.GetActiveScene();
            if (FlashScreen.HasInstance(currentScene))
            {
                FlashScreen.Instance(currentScene).Display("TOMATO spell unlocked in shop!!");
            }
        }
        if (defeatedEnemies.Count==5)
        {
            AllEnemiesKilled=true;
            Scene currentScene = SceneManager.GetActiveScene();
            if (FlashScreen.HasInstance(currentScene))
            {
                FlashScreen.Instance(currentScene).Display("Looking at the sea, the deer rests.",5f);
            }
        }
    }

    public bool IsEnemyDefeated(string id)
    {
        return defeatedEnemies.Contains(id);

    }
    public bool IsTomatoUnlocked()
    {
        return tomatounlocked;
    }
    public bool AreAllEnnemiesDead()
    {
        return AllEnemiesKilled;
    }
    public void TutoDone()
    {
        tutodone = true;
    }
    public bool isTutoDone()
    {
        return tutodone;
    }
}
