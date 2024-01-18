using UnityEngine;

namespace Abilities.Attributes
{
    [CreateAssetMenu(menuName = "Abilities/HighJump", fileName = "High Jump Ability")]
    public class HighJumpAbility : BaseAbility
    {

        public float jumpForce = 10f;
        public ForceMode2D forceMode = ForceMode2D.Impulse;

        public override void Activate(AbilityHolder holder)
        {
            Rigidbody2D body = holder.GetComponent<Rigidbody2D>();
            if (body == null)
            {
                return;
            }
            body.AddForce(new Vector2(0f, jumpForce), forceMode);
        }
    }
}