using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2SpawnedBrick : Spawner
{
    [Header("Player_2SpawnedBrick")]
    private static Player_2SpawnedBrick instance;
    public static Player_2SpawnedBrick Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (Player_2SpawnedBrick.instance != null) Debug.LogError("Only 1 Player_2SpawnedBrick allow to exist");
        Player_2SpawnedBrick.instance = this;
    }
}
