using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterState currentState = CharacterState.Idle;

    public CharacterState CurrentState
    {
        get => currentState;
        private set => currentState = value;
    }

    public void SetCharacterState(CharacterState newState) => CurrentState = newState;

}
