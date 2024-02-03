using System;
using Abilities.Attributes;
using Abilities.System;
using Checks;
using Controllers;
using UnityEngine;

namespace Capabilities {
    [RequireComponent(typeof(Controller), typeof(CollisionDataRetriever), typeof(Rigidbody2D))]
    public class Move : MonoBehaviour
    {
        [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
        [SerializeField, Range(0f, 100f)] private float maxIdleSpeed = 4f;
        [SerializeField, Range(0f, 100f)] private float maxRunningSpeed = 8f;
        [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
        [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;
        [SerializeField, Range(0f, 100f)] private float maxRunningAcceleration = 70f;

        private Vector2 direction;
        private Vector2 desiredVelocity;
        private Vector2 velocity;
        private Rigidbody2D body;
        private Controller controller;
        private CollisionDataRetriever ground;
        private AbilityHolder abilityHolder;

        private float maxSpeedChange;
        private float acceleration;
        private bool onGround;

        // Start is called before the first frame update
        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            ground = GetComponent<CollisionDataRetriever>();
            controller = GetComponent<Controller>();
            abilityHolder = GetComponent<AbilityHolder>();
        }

        // Update is called once per frame
        void Update()
        {
            direction.x = controller.input.RetrieveMoveInput(this.gameObject);
            desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.Friction, 0f);
            if (direction.x < 0) 
            {
                transform.Rotate(0, 180, 0);
            }
        }

        private void FixedUpdate()
        {
            onGround = ground.OnGround;
            velocity = body.velocity;

            if (controller.input.RetrieveRunInput(this.gameObject))
            {
                acceleration = onGround ? maxRunningAcceleration : maxAirAcceleration;
                maxSpeed = maxRunningSpeed;
            } 
            else{
                acceleration = onGround ? maxAcceleration : maxAirAcceleration;
                maxSpeed = maxIdleSpeed;
            }
            maxSpeedChange = acceleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

            body.velocity = velocity;
        }

    }
}
