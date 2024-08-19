using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : GameMonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (GameManager.instance != null) Debug.LogError("Only 1 GameManager allow to exist");
        GameManager.instance = this;

        Time.timeScale = 1.0f;
    }

    public virtual void WinGame()
    {
        EndGame(true);
    }

    public virtual void LoseGame()
    {
        EndGame(false);
    }

    public void EndGame(bool hasWon)
    {
        Time.timeScale = 0.01f;

        if (hasWon)
        {
            WinGamePn.Instance.Open();

        }
        else
        {
            LoseGamePn.Instance.Open();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}