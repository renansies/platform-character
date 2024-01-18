using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AbilityHolder: MonoBehaviour
{
    public Character Owner;
    public BaseAbility Ability;
    public AbilityState CurrentAbilityState = AbilityState.ReadyToUse;
    public IEnumerator handleAbilityUsage;
    public UnityEvent OnTriggerAbility;

    public enum AbilityState
    {
        ReadyToUse = 0,
        Casting = 1,
        Cooldown = 2
    }

    public void TriggerAbility()
    {
        if (CurrentAbilityState != AbilityState.ReadyToUse)
        {
            return;
        }
        if (!CharacterIsOnAllowedState())
        {
            return;
        }
        handleAbilityUsage = HandleAbilityUsage_CO();
        StartCoroutine(HandleAbilityUsage_CO());
    }

    public IEnumerator HandleAbilityUsage_CO()
    {
        CurrentAbilityState = AbilityState.Casting;
        yield return new WaitForSeconds(Ability.CastingTime);
        Ability.Activate(this);
        CurrentAbilityState = AbilityState.Cooldown;
        OnTriggerAbility?.Invoke();
        if (Ability.HasCooldown)
        {
            StartCoroutine(HandleCooldown_CO());
        }
    }

    private IEnumerator HandleCooldown_CO()
    {
        yield return new WaitForSeconds(Ability.Cooldown);
        CurrentAbilityState = AbilityState.ReadyToUse;
    }

    public bool CharacterIsOnAllowedState()
    {
        return Ability.AllowedCharacterStates.Contains(Owner.CurrentState);
    }
}