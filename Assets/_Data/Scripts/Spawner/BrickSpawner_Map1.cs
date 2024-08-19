using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner_Map1 : Spawner
{
    [Header("BrickSpawner_Map1")]
    private static BrickSpawner_Map1 instance;
    public static BrickSpawner_Map1 Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (BrickSpawner_Map1.instance != null) Debug.LogError("Only 1 BrickSpawner_Map1 allow to exist");
        BrickSpawner_Map1.instance = this;
    }
}
