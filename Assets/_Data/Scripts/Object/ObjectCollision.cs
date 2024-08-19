using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ObjectCollision : GameMonoBehaviour
{

    [SerializeField] GameTag.Tag targetTag = GameTag.Tag.Player;
    [SerializeField] Transform holder;
    [SerializeField] Vector3 lastBrickLocalPosition;
    [SerializeField] Vector3 spawnLocalPosition;
    [SerializeField] AnimationManager animationManager;
    [SerializeField] float heightOffset = 0.5f;

    [SerializeField] List<Transform> bricks = new List<Transform>();

    public List<Transform> Bricks => bricks;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHolder();
        this.LoadAnimator();
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("PlayerSpawnedBrick").Find("Holder").transform;
        Debug.LogWarning(transform.name + ": LoadHolder", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animationManager != null) return;
        this.animationManager = transform.GetComponent<AnimationManager>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected override void Start()
    {
        lastBrickLocalPosition = new Vector3(0, holder.localPosition.y, 0);
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player)) ||
            hit.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player_1)) ||
            hit.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player_2)) ||
            hit.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player_3)))
        {
            ObjectCollision otherObject = hit.collider.GetComponentInParent<ObjectCollision>();
            if (otherObject != null && this.bricks.Count < otherObject.bricks.Count)
            {
                UseBrick();
                StartFallingAnimation();
            }
        }
    }

    protected virtual void StartFallingAnimation()
    {
        animationManager.StartFalling();
        Invoke(nameof(StopFallingAnimation), 1.5f);
    }

    protected virtual void StopFallingAnimation()
    {
        animationManager.StopFalling();
    }    

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.gameObject.CompareTag(GameTag.ToString(targetTag))) return;
        SpawnBrickOnPlayer(collision.transform.parent.name);
    }

    protected virtual void ResetBrick()
    {

    }

    protected virtual void SpawnBrickOnPlayer(string brickPrefab)
    {
        spawnLocalPosition = lastBrickLocalPosition;
        spawnLocalPosition.y += heightOffset;
        Vector3 spawnPosition = holder.TransformPoint(spawnLocalPosition);
        Quaternion quaternion = holder.rotation;
        Transform brick = SpawnBrick(brickPrefab, spawnPosition, quaternion);
        if (brick == null) return;

        brick.gameObject.SetActive(true);
        brick.transform.SetParent(holder);

        bricks.Add(brick);
        lastBrickLocalPosition.y += heightOffset;
    }

    protected abstract Transform SpawnBrick(string brickPrefab, Vector3 spawnPosition, Quaternion quaternion);


    public bool HasBricks()
    {
        return bricks.Count > 0;
    }

    public Color GetBrickColor()
    {
        if (bricks.Count > 0)
        {
            return bricks[0].GetComponent<Renderer>().material.color;
        }
        return Color.clear;
    }

    public void UseBrick()
    {
        if (bricks.Count > 0)
        {
            lastBrickLocalPosition.y -= heightOffset;
            int lastIndex = bricks.Count - 1;
            Transform brick = bricks[lastIndex];
            bricks.RemoveAt(lastIndex);

            BrickDespawn brickDespawn = brick.GetComponentInChildren<BrickDespawn>();
            brickDespawn.GetCanDespawn();
        }
    }
}
