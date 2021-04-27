using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveEntity : MonoBehaviour
{
    private float health;
    private float maxHealth = 1;
    public Animator animator;
    public Rigidbody2D body;
    public HealthBar healthBar;

    protected void AliveUpdate()
    {
        Move();
        TakeDamage();
    }
    
    protected void Move()
    {
        var horizontalVelocity = GetHorizontalVelocity();
        var verticalVelocity = GetVerticalVelocity();
        
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
        healthBar.SetMaxHealth(maxHealth);
    }

    protected void ChangeHealthAmount(float change)
    {
        if (change + health <= 0)
            health = 0;
        else if (health + change >= maxHealth)
            health = maxHealth;
        else
            health += change;
        healthBar.SetHealth(health);
    }

    protected virtual float GetHorizontalVelocity()
    {
        return 0;
    }

    protected virtual float GetVerticalVelocity()
    {
        return 0;
    }
    
    protected virtual void TakeDamage()
    {
        
    }
}
