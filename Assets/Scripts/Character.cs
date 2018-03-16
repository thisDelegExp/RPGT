using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
public abstract class Character : MonoBehaviour
{

    [SerializeField]
    protected float speed;
    protected Vector2 direction;
    protected Animator animator;
    private Rigidbody2D myRigidbody;
    public bool IsMoving { get { return direction.x != 0 || direction.y != 0; } }
    protected bool IsAttacking = false;
    protected Coroutine castCoroutine;
    [SerializeField]
    protected Transform hitBox;
    [SerializeField]
    protected Stat health;
    public Stat GetHealth { get { return health; } }
    [SerializeField]
    private float initialHealthValue;
    [SerializeField]
    private float maxHealthValue;

    protected virtual void Start ()
    {
        health.Initialize(initialHealthValue, maxHealthValue);
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	
	protected virtual void  Update ()
    {
        HandleLayers();
        
	}

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Physics based character movement 
    /// </summary>
    public void Move()
    {
        myRigidbody.velocity = direction.normalized * speed; //normalizing direction vector to make movement speed the same for all directions
        //transform.Translate(direction * speed * Time.deltaTime);
       
    }

    
    /// <summary>
    /// Handles animation layers acording to character`s actions
    /// </summary>
    private void HandleLayers()
    {
        if (IsMoving)
        {
            
            ActivateLayer("Walk");
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
            StopAttack();
        }
        else if (IsAttacking)
        {
            ActivateLayer("Attack");
        }
        else
        {
            ActivateLayer("Idle");
        }
    }

    /// <summary>
    /// Switches choosen layer weight as 1 and others as 0
    /// </summary>
    /// <param name="layerName">Name of the layer in animator</param>
    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }
        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    }

    public virtual void StopAttack()
    {
        if (castCoroutine != null)
        {
            StopCoroutine(castCoroutine);
            IsAttacking = false;
            animator.SetBool("cast", IsAttacking);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        //reduce health
        health.CurrentValue -= damage;
        if (health.CurrentValue <= 0)
        {
            animator.SetTrigger("Die"); //die
        }
    }
}
