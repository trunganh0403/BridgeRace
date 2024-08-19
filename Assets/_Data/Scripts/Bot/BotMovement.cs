using UnityEngine;
using UnityEngine.AI;

public abstract class BotMovement : GameMonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected AnimationManager animatorManager;
    [SerializeField] protected float moveSpeed = 1.5f;
    [SerializeField] protected int bricksCollected = 0;
    [SerializeField] protected int bricksNeededToClimb = 300;
    [SerializeField] protected float detectionRadius = 9f;

    [SerializeField] protected GameObject targetBrick = null;
    [SerializeField] protected Transform bridge;
    [SerializeField] protected ObjectCollision objectCollision;

    [SerializeField] protected Vector3 randomDirection;
    [SerializeField] protected float randomMoveTime = 2f;
    [SerializeField] protected float randomMoveTimer;
    [SerializeField] protected Vector3 areaCenter;
    [SerializeField] protected float areaRadius = 10f;

    [SerializeField] protected BridgeTargetManager bridgeTargetManager;
    [SerializeField] protected Transform currentTargetBridge;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNavMeshAgent();
        this.LoadAnimator();
        this.LoadObjectCollision();
    }

    protected virtual void LoadNavMeshAgent()
    {
        if (this.agent != null) return;
        this.agent = transform.parent.GetComponent<NavMeshAgent>();
        Debug.LogWarning(transform.name + ": LoadNavMeshAgent", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animatorManager != null) return;
        this.animatorManager = transform.parent.GetComponent<AnimationManager>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadObjectCollision()
    {
        if (this.objectCollision != null) return;
        this.objectCollision = transform.parent.GetComponent<ObjectCollision>();
        Debug.LogWarning(transform.name + ": LoadObjectCollision", gameObject);
    }

    protected override void Start()
    {
        agent.speed = moveSpeed;
        randomMoveTimer = randomMoveTime;
        areaCenter = transform.parent.position;
    }

    protected virtual void Update()
    {
        ApplyMovement();
        HandleMovement();
    }

    protected virtual void ApplyMovement()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            animatorManager.StopRunning();
        }
    }

    protected virtual void HandleMovement()
    {
        if (animatorManager.IsRunning == false) return;

        if (objectCollision.Bricks.Count <= 0)
        {
            bricksCollected = 0;
            DetectBrick();
        }
        else if (bricksCollected >= bricksNeededToClimb)
        {
            MoveToBridge();
            return;
        }
        else if (targetBrick == null)
        {
            DetectBrick();
        }

        if (targetBrick != null)
        {
            MoveToTarget(targetBrick.transform.position);
        }
        else
        {
            RandomMovement();
        }
    }



    protected abstract void DetectBrick();

    protected virtual void MoveToTarget(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
        animatorManager.StartRunning();

        if (IsNearTarget(targetPosition))
        {
            CollectBrick();
        }
        else if (IsOutOfRange(targetPosition))
        {
            targetBrick = null;
        }
    }

    protected virtual void RandomMovement()
    {
        randomMoveTimer -= Time.deltaTime;

        if (randomMoveTimer <= 0f)
        {
            randomDirection = GenerateRandomDirection();
            randomMoveTimer = randomMoveTime;
        }

        agent.SetDestination(transform.position + randomDirection);
        animatorManager.StartRunning();
    }

    protected virtual Vector3 GenerateRandomDirection()
    {
        Vector3 randomPoint;

        do
        {
            randomPoint = GetRandomPointWithinArea();
        } while (!IsWithinArea(randomPoint));

        return (randomPoint - transform.position).normalized;
    }

    protected virtual Vector3 GetRandomPointWithinArea()
    {
        Vector3 randomPoint = new Vector3(
            Random.Range(-1f, 1f),
            0f,
            Random.Range(-1f, 1f)
        ).normalized * Random.Range(0f, areaRadius);

        return randomPoint + areaCenter;
    }

    protected virtual bool IsWithinArea(Vector3 point)
    {
        return Vector3.Distance(point, areaCenter) <= areaRadius;
    }

    protected virtual bool IsNearTarget(Vector3 targetPosition)
    {
        return Vector3.Distance(transform.parent.position, targetPosition) < 0.5f;
    }

    protected virtual bool IsOutOfRange(Vector3 targetPosition)
    {
        return Vector3.Distance(transform.parent.position, targetPosition) > detectionRadius;
    }

    protected virtual void CollectBrick()
    {
        bricksCollected++;
        targetBrick = null;
    }

    protected virtual void MoveToBridge()
    {
        if (currentTargetBridge == null)
        {
            currentTargetBridge = bridgeTargetManager.GetNextBridgeTarget();
            if (currentTargetBridge == null) return; 
        }

        agent.SetDestination(currentTargetBridge.position);
        animatorManager.StartRunning();

        if (IsNearTarget(currentTargetBridge.position))
        {
            currentTargetBridge = null;
            bricksCollected = 0; 

            if (!bridgeTargetManager.MoveToNextLevel())
            {
                Debug.Log("Bot Win");
            }
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.parent.position, detectionRadius);
    }
}
