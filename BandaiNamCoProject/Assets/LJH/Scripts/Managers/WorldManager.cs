using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainState
{
    Pause, Play, GameFinish
}

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;

    private MainState mainState;

    public MainState MainState
    {
        get { return mainState; }
        set
        {
            switch (value)
            {
                case MainState.Pause:
                    Time.timeScale = 0f;
                    break;
                case MainState.Play:
                    Time.timeScale = 1f;
                    break;
                case MainState.GameFinish:
                    Time.timeScale = 0f;
                    foreach (var manager in GetComponentsInChildren<ManagerBase>())
                        manager.GameOver();
                    break;
            }
            mainState = value;
        }
    }


    private void Awake()
    {
        Instance = this;

        foreach (var manager in GetComponentsInChildren<ManagerBase>())
        {
            manager.Init();
        }
    }
}
