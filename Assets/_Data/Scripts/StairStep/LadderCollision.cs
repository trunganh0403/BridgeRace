using UnityEngine;

public class LadderCollision : GameMonoBehaviour
{
    [SerializeField] private Renderer stepRenderer;
    [SerializeField] private BoxCollider stepCollider;
    [SerializeField] private Color currentBrickColor;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRenderer();
        LoadCollider();
    }

    protected virtual void LoadRenderer()
    {
        if (stepRenderer != null) return;
        stepRenderer = GetComponent<Renderer>();
        Debug.LogWarning($"{name}: Renderer loaded", gameObject);
    }

    protected virtual void LoadCollider()
    {
        if (stepCollider != null) return;
        stepCollider = GetComponent<BoxCollider>();
        Debug.LogWarning($"{name}: Collider loaded", gameObject);
    }

    protected override void Start()
    {
        stepRenderer.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsValidPlayerCollision(collision)) return;

        var playerCollision = collision.gameObject.GetComponent<ObjectCollision>();
        if (playerCollision == null || !playerCollision.HasBricks()) return;

        HandleStepActivation(playerCollision);
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerCollision = other.GetComponent<ObjectCollision>();
        if (playerCollision == null || !playerCollision.HasBricks()) return;

        var playerBrickColor = playerCollision.GetBrickColor();

        if (playerBrickColor == currentBrickColor) return;
        ChangeStepColor(playerBrickColor);
        currentBrickColor = playerBrickColor;
        playerCollision.UseBrick();
    }

    private bool IsValidPlayerCollision(Collision collision)
    {
        return collision.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player))
            || collision.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player_1))
            || collision.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player_2))
            || collision.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player_3));
    }

    private void HandleStepActivation(ObjectCollision playerCollision)
    {
        stepRenderer.enabled = true;
        stepCollider.isTrigger = true;
        currentBrickColor = playerCollision.GetBrickColor();
        ChangeStepColor(currentBrickColor);
        playerCollision.UseBrick();
    }

    private void ChangeStepColor(Color color)
    {
        stepRenderer.material.color = color;
    }
}
