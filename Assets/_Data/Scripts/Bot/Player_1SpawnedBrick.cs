using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_1SpawnedBrick : Spawner
{
    [Header("Player_1SpawnedBrick")]
    private static Player_1SpawnedBrick instance;
    public static Player_1SpawnedBrick Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (Player_1SpawnedBrick.instance != null) Debug.LogError("Only 1 Player_1SpawnedBrick allow to exist");
        Player_1SpawnedBrick.instance = this;
    }
}
