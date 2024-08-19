using UnityEngine;
using System.Collections.Generic;

public class BridgeTargetManager : MonoBehaviour
{
    [SerializeField] private List<Transform> map1BridgePoints; // Danh sách các điểm cầu của map 1
    [SerializeField] private List<Transform> map2BridgePoints; // Danh sách các điểm cầu của map 2

    [SerializeField] List<Transform> currentTargetPoints;
    [SerializeField] int currentLevel = 0; // 0 = Map 1, 1 = Map 2

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        currentLevel = 0;
        currentTargetPoints = new List<Transform>(map1BridgePoints);
    }

    public Transform GetNextBridgeTarget()
    {
        if (currentTargetPoints.Count == 0) return null;

        int randomIndex = Random.Range(0, currentTargetPoints.Count);
        return currentTargetPoints[randomIndex];
    }

    public bool MoveToNextLevel()
    {
        if (currentLevel == 0)
        {
            currentTargetPoints = new List<Transform>(map2BridgePoints);
            currentLevel++;
            return true;
        }
        return false; // Đã hoàn thành tất cả các màn chơi
    }
}
