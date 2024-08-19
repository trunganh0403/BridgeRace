using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BrickDespawn : Despawn
{
    [SerializeField] protected GameTag.Tag targetTag = GameTag.Tag.Player;
    [SerializeField] protected bool canDespawn = false;


    protected override bool CanDespawn()
    {
        return canDespawn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(GameTag.ToString(targetTag))) return;
        canDespawn = true;
    }

    public virtual void GetCanDespawn()
    {
        canDespawn = true;
    }    
}
