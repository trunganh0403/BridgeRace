using UnityEngine;

public class DoorTrigger : GameMonoBehaviour
{
    [SerializeField] Vector3 openOffset = Vector3.right;
    [SerializeField] Vector3 initialPosition;
    [SerializeField] Renderer stepRenderer;
    [SerializeField] BrickRandom_Map2 brickRandomMap2;
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] float speed = 2f;
    [SerializeField] bool isPlayerNear = false;
    [SerializeField] bool collisionHandled = false;

    public delegate void PlayerPassedHandler();
    public static event PlayerPassedHandler OnPlayerPassed;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBrickRandom_Map2();
    }

    protected virtual void LoadBrickRandom_Map2()
    {
        if (this.brickRandomMap2 != null) return;
        this.brickRandomMap2 = FindAnyObjectByType<BrickRandom_Map2>();
        Debug.LogWarning(transform.name + ": LoadBrickRandom_Map2", gameObject);
    }

    protected override void Start()
    {
        initialPosition = transform.position;
    }

    //void Update()
    //{
    //    //Vector3 targetPosition = isPlayerNear ? initialPosition + openOffset : initialPosition;
    //    //transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //if (collisionHandled) return;

        string nameBrick = "";

        if (collision.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player)))
        {
            nameBrick = GameTag.ToString(GameTag.Tag.Brick_1);
            UpdatePlayerStatus(nameBrick);
        }
        else if (collision.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player_1)))
        {
            
            nameBrick = GameTag.ToString(GameTag.Tag.Brick_2);
            UpdatePlayerStatus(nameBrick);
        }
        else if (collision.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player_2)))
        {

            nameBrick = GameTag.ToString(GameTag.Tag.Brick_3);
            UpdatePlayerStatus(nameBrick);
        }
        else if (collision.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player_3)))
        {

            nameBrick = GameTag.ToString(GameTag.Tag.Brick_4);
            UpdatePlayerStatus(nameBrick);
        }
       
    }

    protected virtual void UpdatePlayerStatus(string name)
    {
        collisionHandled = true;
        brickRandomMap2.SetNameBrick(name);
        OnPlayerPassed?.Invoke();  
        stepRenderer.enabled = false;
        boxCollider.isTrigger = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionHandled = false;
    }
}
