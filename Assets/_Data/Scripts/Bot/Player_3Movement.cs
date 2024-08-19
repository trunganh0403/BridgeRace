using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_3Movement : BotMovement
{
    protected override void DetectBrick()
    {
        Collider[] hits = Physics.OverlapSphere(transform.parent.position, detectionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Brick_4"))
            {
                targetBrick = hit.gameObject;
                break;
            }
        }
    }
}
