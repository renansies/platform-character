using Abilities.System;
using UnityEngine;

namespace Abilities.Attributes
{
    [CreateAssetMenu(menuName = "Abilities/Spider Grip", fileName = "Spider Grip Ability")]
    public class SpiderGripAbility : BaseAbility
    {

        [SerializeField] private LayerMask layer;
        [SerializeField] private PhysicsMaterial2D material;
        public override void Activate(AbilityHolder holder)
        {
            Rigidbody2D body = holder.GetComponent<Rigidbody2D>();
            if (body == null)
            {
                return;
            }
            if (layer.value == 7)
            {
                body.gravityScale = 0;
            }
        }
    }
}