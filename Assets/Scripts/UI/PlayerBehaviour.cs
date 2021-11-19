using UnityEngine.Events;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //This handles all player behaviour, like jumping, bouncing on walls, dying, etc.
    [Header("Movement Values")]
    public float jumpStrength;
    public Vector3 initialJumpDirection;

    [Header("Events")]
    public UnityEvent onHitWall;
    public UnityEvent onPlayerDead;

    private bool buttonPress;
    private bool wallHit;
    private Rigidbody2D rb2d;
    private float grav;
    private Vector3 jumpForce;
    private SpriteRenderer sprite;
    private Vector3 jumpDirection;
    private Collider2D bodyCollider;
    
    void Awake()
    {
        rb2d        = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<Collider2D>();

        grav              = rb2d.gravityScale; //Store initial gravityScale, so you can set it back once the game starts.
        rb2d.gravityScale = 0; //The character won't start falling until you press the jump button/touch the screen.
        jumpDirection     = initialJumpDirection; //Initializes the jumping direction to the Vector3 set on the inspector.
        wallHit           = false;
    }
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // The Update function only assigns direction and strength of jumps, adding forces is 
    // handled im FixedUpdate.
    void Update()
    {
        buttonPress = Input.GetButtonDown("Jump") || Input.touchCount > 0;

        if (buttonPress)
            jumpForce = jumpDirection.normalized * jumpStrength;
        else
            jumpForce = Vector3.zero;
    }

    void FixedUpdate ()
    {
        if (jumpForce != Vector3.zero)
            Jump(jumpForce);
        else if (wallHit) 
            WallBounce();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Spikes":
                Die();
                break;
            case "Wall":
                OnEnterWall(collider.gameObject);
                break;
        }
    }
    
    // Changes player direction and invokes the callback assigned for onHitWall in the inspector.
    // Also sets wallHit flag, so that the next FixedUpdate knows it's time to apply forces
    void OnEnterWall(GameObject wall)
    {
        onHitWall.Invoke();
        if(wall.transform.position.x < 0)
            jumpDirection = new Vector3(-initialJumpDirection.x, jumpDirection.y, jumpDirection.z);
        else if (wall.transform.position.x > 0)
            jumpDirection = new Vector3(initialJumpDirection.x, jumpDirection.y, jumpDirection.z);

        sprite.flipX = !sprite.flipX;
        wallHit = true;
    }

    // Aplies a force to the rigidbody2D attached to this gameObject, opposite to the last wall hit,
    // while conserving vertical momentum, resets wallHit flag.
    void WallBounce()
    {
        Vector3 bounceVector = new Vector3(jumpDirection.normalized.x * jumpStrength, 0, 0);
        
        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        rb2d.AddForce(bounceVector);

        wallHit = false;
    }

    void Jump (Vector3 jumpForce)
    {
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(jumpForce);
    }

    void Die()
    {
        gameObject.SetActive(false);
        onPlayerDead.Invoke();
    }
    // StartPlayer is called once the game is in Gameplay state, 
    // sets the gravity of the rb2D to it's original value.
    public void StartPlayer()
    {
        rb2d.gravityScale = grav;
    }

    // ResetPlayer is called once the player returns to the Title Screen, resets the position, 
    // gravityScale, jumpDirection, etc. So that the game is ready to go into Gameplay mode again.
    public void ResetPlayer()
    {
        transform.position = Vector3.zero;
        sprite.flipX = false;
        gameObject.SetActive(true);
        rb2d.gravityScale = 0;
        jumpDirection = initialJumpDirection;
    }
}
