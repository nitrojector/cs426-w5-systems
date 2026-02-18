using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static int Score { get; set; }
    public static int HiScore { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void UpdateScore(int points)
    {
        Score = points;
        if (Score > HiScore)
        {
            HiScore = Score;
        }
    }

    void Update()
    {
        if (Keyboard.current.rKey.isPressed)
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            CakePlayer.Instance?.Reset();
            ActorManager.DestroyAll();
        }

        if (Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }
    }
}