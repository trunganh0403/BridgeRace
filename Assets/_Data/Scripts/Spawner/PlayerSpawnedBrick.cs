using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnedBrick : Spawner
{
    [Header("PlayerSpawnedBrick")]
    private static PlayerSpawnedBrick instance;
    public static PlayerSpawnedBrick Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (PlayerSpawnedBrick.instance != null) Debug.LogError("Only 1 PlayerSpawnedBrick allow to exist");
        PlayerSpawnedBrick.instance = this;
    }
}
