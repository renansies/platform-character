using Controllers;
using UnityEngine;

namespace Capabilities
{
    [RequireComponent(typeof(Controller))]
    public class Shoot : MonoBehaviour
    {

        public Transform ShootingPoint;
        public GameObject projectile;

        private Controller controller;
        private bool desiredShoot;
        private bool isShootReset;


        void Awake()
        {
            controller = GetComponent<Controller>();
        }

        // Update is called once per frame
        void Update()
        {
            desiredShoot = controller.input.RetrieveFireInput(this.gameObject);
            Flip();
        }

        void FixedUpdate() 
        {
            if (desiredShoot && !isShootReset)
            {
                FireAction();
                isShootReset = true;
            }
            if (!desiredShoot)
            {
                isShootReset = false;
            }
        }

        private void FireAction()
        {
            Instantiate(projectile, ShootingPoint.position, ShootingPoint.rotation);     
        }

        private void Flip()
        {
            if (transform.localScale.x < 0) 
            {
                ShootingPoint.localRotation = new Quaternion(0, 180, 0, 0);
            }
            if (transform.localScale.x > 0)
            {
                ShootingPoint.localRotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }
}
