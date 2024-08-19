using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BrickRandom : GameMonoBehaviour
{
    [Header("BrickRandom")]
    [SerializeField] protected List<Transform> bricks;

    [SerializeField] protected int width = 15;
    [SerializeField] protected int height = 5;
    [SerializeField] protected float spacing = 0.5f;

    [SerializeField] protected bool isPositionOccupied;

    protected override void Start()
    {
        InitializeBrickGrid();
    }

    protected virtual void FixedUpdate()
    {
        SpawnPrefab();
    }

    protected virtual void InitializeBrickGrid()
    {
        Vector3 startPosition = transform.position;

        for (int row = 0; row < this.height; row++)
        {
            for (int col = 0; col < this.width; col++)
            {
                Vector3 position = startPosition + new Vector3(col * spacing, 0, row * spacing);
                Transform brick = SpawnBricks(position);
                bricks.Add(brick);
            }
        }
    }

    protected abstract Transform SpawnBricks(Vector3 position);
    

    protected virtual void SpawnPrefab()
    {
        List<Transform> bricksToRemove = new List<Transform>();
        foreach (Transform brick in bricks)
        {
            if (!brick.gameObject.activeInHierarchy)
            {
                bricksToRemove.Add(brick);
                StartCoroutine(RespawnBrickAfterDelay(brick.position, 3f));
            }
        }

        foreach (Transform brick in bricksToRemove)
        {
            bricks.Remove(brick);
        }
    }

    IEnumerator RespawnBrickAfterDelay(Vector3 position, float delay)
    {
        yield return new WaitForSeconds(delay);

        isPositionOccupied = IsPositionOccupied(position);

        if (!isPositionOccupied)
        {
            Transform brick = SpawnBricks(position);
            if (brick != null)
            {
                bricks.Add(brick);
            }
        }
    }

    protected virtual bool IsPositionOccupied(Vector3 position)
    {
        foreach (Transform brick in bricks)
        {
            if (brick.gameObject.activeInHierarchy && Vector3.Distance(brick.position, position) < 0.01f)
            {
                return true;
            }
        }
        return false;
    }
}