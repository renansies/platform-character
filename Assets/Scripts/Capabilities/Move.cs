using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;
    [SerializeField, Range(0.05f, 0.5f)] private float wallStickTime = 0.25f;

    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D body;
    private CollisionDataRetriever ground;
    private WallInteractor wallInteractor;

    private float maxSpeedChange;
    private float acceleration;
    private float wallStickCounter;
    private bool onGround;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<CollisionDataRetriever>();
        wallInteractor = GetComponent<WallInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = input.RetrieveMoveInput();
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.Friction, 0f);
    }

    private void FixedUpdate()
    {
        onGround = ground.OnGround;
        velocity = body.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        #region Wall Stick
        if (ground.OnWall && !ground.OnGround && !wallInteractor.WallJumping)
        {
            if (wallStickCounter > 0)
            {
                velocity.x = 0;
                if (input.RetrieveMoveInput() == ground.ContactNormal.x)
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
}

