using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn : GameMonoBehaviour
{
    protected virtual void FixedUpdate()
    {
        this.Despawning();
    }

    protected virtual void Despawning()
    {
        if (!this.CanDespawn()) return;
        this.DespawnObject();
    }
    public virtual void DespawnObject()
    {
        // For override
    }

    protected abstract bool CanDespawn();
}
