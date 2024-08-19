using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickRandom_Map1 : BrickRandom
{
  protected override Transform SpawnBricks(Vector3 position)
    {
        Transform brickRandom = BrickSpawner_Map1.Instance.RandomPrefab();
        Quaternion quaternion = transform.rotation;
        Transform brick = BrickSpawner_Map1.Instance.Spawn(brickRandom, position, quaternion);

        if (brick != null)
        {
            brick.gameObject.SetActive(true);
        }
        return brick;
    }
}