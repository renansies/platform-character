using UnityEngine;
[RequireComponent(typeof(Controller), typeof(CollisionDataRetriever), typeof(Rigidbody2D))]
public class WallInteractor : MonoBehaviour
{

    public bool WallJumping { get; private set; }

    [Header("Wall Slide")]
    [SerializeField, Range(0.1f, 5f)] private float wallSlideMaxSpeed = 2f;
    [Header("WallJump")]
    [SerializeField] private Vector2 wallJumpClimb = new Vector2(4f, 12f);
    [SerializeField] private Vector2 wallJumpBounce = new Vector2(10.7f, 10f);
    [SerializeField] private Vector2 wallJumpLeap = new Vector2(14f, 12f);
    [SerializeField, Range(0.05f, 0.5f)] private float wallStickTime = 0.25f;


    private CollisionDataRetriever collisionDataRetriever;
    private Rigidbody2D body;
    private Controller controller;
    private Vector2 velocity;
    private bool onWall;
    private bool onGround;
    private bool desiredJump;
    private float wallDirectionX;
    private float wallStickCounter;

    // Start is called before the first frame update
    void Start()
    {
        collisionDataRetriever = GetComponent<CollisionDataRetriever>();
        body = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if(onWall && ! onGround)
        {
            desiredJump |= controller.input.RetrieveJumpInput(this.gameObject);
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
            if (-wallDirectionX == controller.input.RetrieveMoveInput(this.gameObject))
            {
                velocity = new Vector2(wallJumpClimb.x * wallDirectionX, wallJumpClimb.y);
                WallJumping = true;
                desiredJump = false;
            }
            else if (controller.input.RetrieveMoveInput(this.gameObject) == 0)
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

        #region Wall Stick
        if (collisionDataRetriever.OnWall && !collisionDataRetriever.OnGround && !WallJumping)
        {
            if (wallStickCounter > 0)
            {
                velocity.x = 0;
                if (controller.input.RetrieveMoveInput(this.gameObject) == collisionDataRetriever.ContactNormal.x)
                {
                    wallStickCounter -= Time.deltaTime;
                }
                else
                {
                    wallStickCounter = wallStickTime;
                }
            }
            else
            {
                wallStickCounter = wallStickTime;
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
