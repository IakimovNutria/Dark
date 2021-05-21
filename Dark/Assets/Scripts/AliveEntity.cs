using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveEntity : MonoBehaviour
{
    public float Health { get; private set; } = 1;

    private float maxHealth = 1;
    [NonSerialized]
    public Animator animator;
    protected Rigidbody2D body;
    public Bar healthBar;
    private bool haveHealthBar;

    public void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        body = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if (Health == 0)
        {
            gameObject.tag = "Died";
            body.velocity = new Vector2(0, 0);
            Move();
            animator.SetBool("isDied", true);
        }
        else
        {
            Move();
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Died"))
            Destroy(gameObject);
    }

    private void Move()
    {
        var horizontalVelocity = Health == 0 ? 0 : GetHorizontalVelocity();
        var verticalVelocity = Health == 0 ? 0 : GetVerticalVelocity();
        
        animator.SetBool("isWalk", !(horizontalVelocity == 0 && verticalVelocity == 0));
        animator.SetBool("isWalkLeft", horizontalVelocity < 0);
        animator.SetBool("isWalkBack", verticalVelocity < 0 && horizontalVelocity == 0);
        animator.SetBool("isWalkRight", horizontalVelocity > 0);
        animator.SetBool("isWalkFront", verticalVelocity > 0 && horizontalVelocity == 0);
        
        body.velocity = new Vector2(horizontalVelocity, verticalVelocity);

        var entityTransform = transform;
        var previousPosition = entityTransform.position;
        var newPosition = new Vector3(previousPosition.x, previousPosition.y, previousPosition.y);
        entityTransform.position = newPosition;
    }

    protected void SetMaxHealth(float maxHealthValue)
    {
        maxHealth = maxHealthValue;
        ChangeHealthAmount(maxHealth);
        healthBar.SetMaxValue(maxHealth);
    }

    public void ChangeHealthAmount(float change)
    {
        if (change + Health <= 0)
            Health = 0;
        else if (Health + change >= maxHealth)
            Health = maxHealth;
        else
            Health += change;

        healthBar.SetValue(Health);
    }

    public void TakeDamage(float damage)
    {
        ChangeHealthAmount(-damage);
    }

    public void SetHealth(float health)
    {
        Health = health;
        healthBar.SetValue(Health);
    }
    
    protected virtual float GetHorizontalVelocity()
    {
        return 0;
    }

    protected virtual float GetVerticalVelocity()
    {
        return 0;
    }
}
