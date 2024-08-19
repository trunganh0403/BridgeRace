using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDespawn_Map1 : BrickDespawn
{
    public override void DespawnObject()
    {
        BrickSpawner_Map1.Instance.Despawn(transform.parent);
        canDespawn = false;
    }
}
