using Abilities.System;
using UnityEngine;

namespace Abilities.Attributes
{
    [CreateAssetMenu(menuName = "Abilities/Run", fileName = "Run")]
    public class RunAbility : BaseAbility
    {

        public float accelarationForce = 1000f;
        public ForceMode2D forceMode = ForceMode2D.Force;

        public override void Activate(AbilityHolder holder)
        {
            Rigidbody2D body = holder.GetComponent<Rigidbody2D>();
            if (body == null)
            {
                return;
            }
            body.AddForce(new Vector2(body.velocity.x * accelarationForce, 0f));
        }
    }
}