using UnityEngine;

public class Player : AliveEntity
{
    public static Player Instance;
    public float MAXPlayerHealth { get; } = 1000f;

    private float speed = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
            Destroy(gameObject);
        
    }

    private new void Start()
    {
        aliveEntity = this;
        aliveEntity.Start();
        SetMaxHealth(MAXPlayerHealth);
    }

    private new void Update()
    {
        aliveEntity.Update();
    }

    protected override Vector2 GetVelocity()
    {
        var horizontalVelocity = (Input.GetKey(GameManager.GM.KeyRight) ? speed : 0) + 
                                 (Input.GetKey(GameManager.GM.KeyLeft) ? -speed : 0) +
                                 body.velocity.x / 10 /*для плавного перехода между анимациями ходьбы*/;
        var verticalVelocity = (Input.GetKey(GameManager.GM.KeyUp) ? speed : 0) + 
                               (Input.GetKey(GameManager.GM.KeyDown) ? -speed : 0) +
                               body.velocity.y / 10 /*для плавного перехода между анимациями ходьбы*/;
        return new Vector2(horizontalVelocity, verticalVelocity);
    }
}