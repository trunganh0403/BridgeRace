using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickRandom_Map2 : BrickRandom
{
    [Header("BrickRandom_Map2")]
    [SerializeField] string brickRandom;
    [SerializeField] bool isSpawn = false;
    [SerializeField] List<string> nameBricks = new List<string>();

    protected override void Start()
    {
        DoorTrigger.OnPlayerPassed += HandlePlayerPassed;
    }

    protected override void FixedUpdate()
    {
        if (!isSpawn) return; 
        SpawnPrefab();
    }

    public void SetNameBrick(string name)
    {
        nameBricks.Add(name);
    }

    private void HandlePlayerPassed()
    {
        InitializeBrickGrid();
        isSpawn = true;

    }

    protected override Transform SpawnBricks(Vector3 position)
    {
        brickRandom = RandomNameBrick();
        Quaternion quaternion = transform.rotation;
        Transform brick = BrickSpawner_Map2.Instance.Spawn(brickRandom, position, quaternion);
        
        if (brick != null)
        {
            brick.gameObject.SetActive(true);
        }
        return brick;
    }

    protected virtual string RandomNameBrick()
    {
        if (nameBricks.Count > 0)
        {
            int rand = Random.Range(0, nameBricks.Count);
            return nameBricks[rand];
        }
        else
        {
            Debug.LogWarning("nameBricks list is empty!");
            return null; 
        }

    }
}