using Abilities.Attributes;
using Abilities.System;
using Checks;
using Controllers;
using UnityEngine;

namespace Capabilities

{
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
        private AbilityHolder abilityHolder;
        private Vector2 velocity;
        private bool onWall;
        private bool onGround;
        private bool desiredJump;
        private bool isJumpReset;
        private float wallDirectionX;
        private float wallStickCounter;

        // Start is called before the first frame update
        void Start()
        {
            collisionDataRetriever = GetComponent<CollisionDataRetriever>();
            body = GetComponent<Rigidbody2D>();
            controller = GetComponent<Controller>();
            abilityHolder = GetComponent<AbilityHolder>();

            isJumpReset = true;
        }

        // Update is called once per frame
        void Update()
        {
            desiredJump = controller.input.RetrieveJumpInput(this.gameObject);
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
            if(onWall && ! onGround)
            {
                if (desiredJump && isJumpReset)
                {
                    if (controller.input.RetrieveMoveInput(this.gameObject) == 0)
                    {
                        velocity = new Vector2(wallJumpBounce.x * wallDirectionX, wallJumpBounce.y);
                        WallJumping = true;
                        desiredJump = false;
                        isJumpReset = false;
                    }
                    else if (Mathf.Sign(-wallDirectionX) == Mathf.Sign(controller.input.RetrieveMoveInput(this.gameObject)))
                    {
                        velocity = new Vector2(wallJumpClimb.x * wallDirectionX, wallJumpClimb.y);
                        WallJumping = true;
                        desiredJump = false;
                        isJumpReset = false;
                    }
                    else
                    {
                        velocity = new Vector2(wallJumpLeap.x * wallDirectionX, wallJumpLeap.y);
                        WallJumping = true;
                        desiredJump = false;
                        isJumpReset = false;
                    }
                }
                else if (!desiredJump)
                {
                    isJumpReset = true;
                }
            }
            #endregion

            #region Wall Stick
            if (collisionDataRetriever.OnWall && !collisionDataRetriever.OnGround && !WallJumping)
            {
                if (wallStickCounter > 0)
                {
                    velocity.x = 0;
                    if (controller.input.RetrieveMoveInput(this.gameObject) != 0 && Mathf.Sign(controller.input.RetrieveMoveInput(this.gameObject)) == Mathf.Sign(collisionDataRetriever.ContactNormal.x))
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
            // if (abilityHolder.Ability?.GetType() != typeof(SpiderGripAbility))
            // {
            //     abilityHolder.TriggerAbility(); 
            // }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collisionDataRetriever.EvaluateCollision(collision);
            isJumpReset = false;

            if (collisionDataRetriever.OnWall && !collisionDataRetriever.OnGround && WallJumping)
            {
                body.velocity = Vector2.zero;
            }
        }
    }
}