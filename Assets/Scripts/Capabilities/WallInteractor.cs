using UnityEngine;

public class WallInteractor : MonoBehaviour
{

    [SerializeField] private InputController input = null;
    public bool WallJumping { get; private set; }

    [Header("Wall Slide")]
    [SerializeField, Range(0.1f, 5f)] private float wallSlideMaxSpeed = 2f;
    [Header("WallJump")]
    [SerializeField] private Vector2 wallJumpClimb = new Vector2(4f, 12f);
    [SerializeField] private Vector2 wallJumpBounce = new Vector2(10.7f, 10f);
    [SerializeField] private Vector2 wallJumpLeap = new Vector2(14f, 12f);

    private CollisionDataRetriever collisionDataRetriever;
    private Rigidbody2D body;
    private Vector2 velocity;
    private bool onWall;
    private bool onGround;
    private bool desiredJump;
    private float wallDirectionX;

    // Start is called before the first frame update
    void Start()
    {
        collisionDataRetriever = GetComponent<CollisionDataRetriever>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(onWall && ! onGround)
        {
            desiredJump |= input.RetrieveJumpInput();
        }
    }

    private void FixedUpdate()
    {
        velocity = body.velocity;
        onWall = collisionDataRetriever.OnWall;
        onGround = collisionDataRetriever.OnGround;
        wallDirectionX = collisionDataRetriever.ContactNormal.x;

        #region Wall Slide
        if (onWall)
        {
            if (velocity.y < -wallSlideMaxSpeed)
            {
                velocity.y = -wallSlideMaxSpeed;
            }
        }
        #endregion

        #region Wall Jump

        if ((onWall && velocity.x == 0) || onGround)
        {
            WallJumping = false;
        }
        if (desiredJump)
        {
            if (-wallDirectionX == input.RetrieveMoveInput())
            {
                velocity = new Vector2(wallJumpClimb.x * wallDirectionX, wallJumpClimb.y);
                WallJumping = true;
                desiredJump = false;
            }
            else if (input.RetrieveMoveInput() == 0)
            {
                velocity = new Vector2(wallJumpBounce.x * wallDirectionX, wallJumpBounce.y);
                WallJumping = true;
                desiredJump = false;
            }
            else
            {
                velocity = new Vector2(wallJumpLeap.x * wallDirectionX, wallJumpLeap.y);
                WallJumping = true;
                desiredJump = false;
            }
        }
        #endregion

        body.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionDataRetriever.EvaluateCollision(collision);

        if (collisionDataRetriever.OnWall && !collisionDataRetriever.OnGround && WallJumping)
        {
            body.velocity = Vector2.zero;
        }
    }
}
