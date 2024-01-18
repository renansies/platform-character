using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
    public class AIController : InputController
    {

        [Header("Interaction")]
        [SerializeField] private LayerMask layer;
        [Header("Ray")]
        [SerializeField] private float bottomDistance = 1f;
        [SerializeField] private float topDistance = 1f;
        [SerializeField] private float xOffset = 1f;

        private RaycastHit2D groundInfoBottom;
        private RaycastHit2D groundInfoTop;

        public override float RetrieveMoveInput(GameObject gameObject)
        {
            groundInfoBottom = Physics2D.Raycast(
                new Vector2(gameObject.transform.position.x + (xOffset * gameObject.transform.localScale.x), 
                            gameObject.transform.position.y), Vector2.down, bottomDistance, layer);
                Debug.DrawRay(new Vector2(gameObject.transform.position.x + (xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y), Vector2.down * bottomDistance, Color.green);

                groundInfoTop = Physics2D.Raycast(
                new Vector2(gameObject.transform.position.x + (xOffset * gameObject.transform.localScale.x), 
                            gameObject.transform.position.y), Vector2.right * gameObject.transform.localScale.x, topDistance, layer);
                Debug.DrawRay(new Vector2(gameObject.transform.position.x + (xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y), Vector2.right * topDistance * gameObject.transform.localScale.x, Color.green);

                if (groundInfoBottom.collider == true || groundInfoTop.collider == false)
                {
                    gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                }
            return gameObject.transform.localScale.x;
        }
        public override bool RetrieveJumpInput(GameObject gameObject)
        {
            return false;
        }

        public override bool RetrieveRunInput(GameObject gameObject)
        {
            return false;
        }
    }
}