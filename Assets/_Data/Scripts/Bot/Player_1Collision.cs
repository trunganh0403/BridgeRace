using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_1Collision : ObjectCollision
{
    protected override Transform SpawnBrick(string brickPrefab, Vector3 spawnPosition, Quaternion quaternion)
    {
        Transform brick = Player_1SpawnedBrick.Instance.Spawn(brickPrefab, spawnPosition, quaternion);
        return brick;
    }
}
