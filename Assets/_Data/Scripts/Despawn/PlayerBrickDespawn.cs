using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrickDespawn : BrickDespawn
{
    public override void DespawnObject()
    {
        PlayerSpawnedBrick.Instance.Despawn(transform.parent);
        canDespawn = false;
    }

}
