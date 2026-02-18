using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static float Score { get; private set; }
    public static float HiScore { get; private set; }

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

    public static void ResetScore()
    {
        Score = 0.0f;
    }

    public static void AdjustScore(float delta)
    {
        Score += delta;
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