using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDespawn_Map2 : BrickDespawn
{
    public override void DespawnObject()
    {
        BrickSpawner_Map2.Instance.Despawn(transform.parent);
        canDespawn = false;
    }
}
