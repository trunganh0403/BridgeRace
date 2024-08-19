using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2Movement : BotMovement
{
    protected override void DetectBrick()
    {
        Collider[] hits = Physics.OverlapSphere(transform.parent.position, detectionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Brick_3"))
            {
                targetBrick = hit.gameObject;
                break;
            }
        }
    }
}
