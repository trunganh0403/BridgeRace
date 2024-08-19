using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderList : MonoBehaviour
{
    [SerializeField] List<LadderCollision> visitedSteps = new List<LadderCollision>();

    public void AddVisitedStep(LadderCollision step)
    {
        if (!visitedSteps.Contains(step))
        {
            visitedSteps.Add(step);
        }
    }

    public bool IsStepVisited(LadderCollision step)
    {
        return visitedSteps.Contains(step);
    }

    public List<LadderCollision> GetVisitedSteps()
    {
        return visitedSteps;
    }
}
