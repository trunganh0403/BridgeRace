using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCollision : ObjectCollision
{
    protected override Transform SpawnBrick(string brickPrefab, Vector3 spawnPosition, Quaternion quaternion)
    {
        Transform brick = PlayerSpawnedBrick.Instance.Spawn(brickPrefab, spawnPosition, quaternion);
        return brick;
    }
}
