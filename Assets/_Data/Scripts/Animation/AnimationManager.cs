using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : GameMonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected bool isRunning = true;
    public bool IsRunning => isRunning;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = transform.GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    public virtual void StartFalling()
    {
        animator.SetBool("Fall", true);
        StopRunning();
        isRunning = false;
    }

    public virtual void StopFalling()
    {
        animator.SetBool("Fall", false);
        isRunning = true;
    }

    public virtual void StartRunning()
    {
        animator.SetBool("Running", true);
    }

    public virtual void StopRunning()
    {
        animator.SetBool("Running", false);
    }    
}
