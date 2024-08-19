using UnityEngine;

public class PlayerMovement : GameMonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] AnimationManager animatorManager;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] Vector3 velocity;
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] float gravity = -9.81f; 

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharacterController();
        this.LoadAnimator();
        this.LoadFixedJoystick();
    }

    protected virtual void LoadCharacterController()
    {
        if (this.characterController != null) return;
        this.characterController = transform.parent.GetComponent<CharacterController>();
        Debug.LogWarning(transform.name + ": LoadCharacterController", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animatorManager != null) return;
        this.animatorManager = transform.parent.GetComponent<AnimationManager>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

     protected virtual void LoadFixedJoystick()
    {
        if (this.joystick != null) return;
        this.joystick = FindAnyObjectByType<FixedJoystick>();
        Debug.LogWarning(transform.name + ": LoadFixedJoystick", gameObject);
    }

    private void FixedUpdate()
    {
        this.Move();
    }

    protected virtual void Move()
    {
        if (animatorManager.IsRunning == false) return;

        Vector3 moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;

        if (characterController.isGrounded)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if (moveDirection != Vector3.zero)
        {
            characterController.Move((moveDirection * moveSpeed * Time.deltaTime) + velocity * Time.deltaTime);

            transform.parent.rotation = Quaternion.LookRotation(moveDirection);
            animatorManager.StartRunning();
        }
        else
        {
            animatorManager.StopRunning();
        }

        characterController.Move(velocity * Time.deltaTime);
    }
}
