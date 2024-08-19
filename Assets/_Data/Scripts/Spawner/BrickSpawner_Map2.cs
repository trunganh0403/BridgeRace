using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner_Map2 : Spawner
{
    [Header("BrickSpawner_Map2")]
    private static BrickSpawner_Map2 instance;
    public static BrickSpawner_Map2 Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (BrickSpawner_Map2.instance != null) Debug.LogError("Only 1 BrickSpawner_Map2 allow to exist");
        BrickSpawner_Map2.instance = this;
    }
}
