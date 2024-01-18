using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public abstract class BaseAbility : ScriptableObject
{
    [Header("Cooldown And Casting Time")]
    public bool HasCooldown = true;
    public float Cooldown = 1f;
    public float CastingTime = 0f;

    [Header("Allowed States")] 
    public List<CharacterState> AllowedCharacterStates = new List<CharacterState>() {CharacterState.Idle};
    public virtual void OnAbilityUpdate(AbilityHolder holder) {}
    public abstract void Activate(AbilityHolder holder);
}
