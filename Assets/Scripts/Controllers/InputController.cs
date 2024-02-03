using UnityEngine;

namespace Controllers
{
    public abstract class InputController : ScriptableObject
    {
        public abstract float RetrieveMoveInput(GameObject gameObject);
        public abstract bool RetrieveJumpInput(GameObject gameObject);
        public abstract bool RetrieveRunInput(GameObject gameObject);
        public abstract bool RetrieveFireInput(GameObject gameObject);
    }
}
