using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_3SpawnedBrick : Spawner
{
    [Header("Player_3SpawnedBrick")]
    private static Player_3SpawnedBrick instance;
    public static Player_3SpawnedBrick Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (Player_3SpawnedBrick.instance != null) Debug.LogError("Only 1 Player_3SpawnedBrick allow to exist");
        Player_3SpawnedBrick.instance = this;
    }
}
