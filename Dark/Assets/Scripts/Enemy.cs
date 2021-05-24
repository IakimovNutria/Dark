using UnityEngine;

public class Enemy : AliveEntity
{
    private AliveEntity aliveEntity;
    private Transform playerTransform;
    private readonly float damageRadius = 0.5f;
    public static float maxEnemyHealth = 40;
    private Player player;
    private void Awake()
    {
        aliveEntity = this;
        aliveEntity.Start();
        var playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerGameObject.transform;
        player = playerGameObject.GetComponent<Player>();
        
        SetMaxHealth(maxEnemyHealth);
    }

    private new void Update()
    { 
        aliveEntity.Update();
        DamagePlayer();
    }

    protected override Vector2 GetVelocity()
    {
        var playerPosition = playerTransform.position;
        var enemyPosition = body.position;

        float horizontalVelocity;
        float verticalVelocity;
        
        if (Geometry.GetLength(playerPosition, enemyPosition) < 0.3)
        {
            horizontalVelocity = 0.00001f * (playerPosition.x > enemyPosition.x ? 1 : -1);
            verticalVelocity = 0.00001f * (playerPosition.y > enemyPosition.y ? 1 : -1);
        }
        else
        {
            var velocity = body.velocity;
            horizontalVelocity = (playerPosition.x - enemyPosition.x) / 2.5f + 
                                 velocity.x / 10 /*для плавного перехода между анимациями ходьбы*/;
            verticalVelocity = (playerPosition.y - enemyPosition.y) / 2.5f + 
                               velocity.y / 10 /*для плавного перехода между анимациями ходьбы*/;
        }
        return new Vector2(horizontalVelocity, verticalVelocity);
    }

    private void DamagePlayer()
    {
        if (Health == 0)
            return;
        var enemyPosition = body.position;
        var playerPosition = playerTransform.position;
        var length = Geometry.GetLength(enemyPosition, playerPosition);
        if (length < damageRadius && !GameManager.GM.isGameFreezed)
            player.TakeDamage(0.75f + length);
    }
}
