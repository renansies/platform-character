using UnityEngine;

namespace Commons
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager gameManager { get; private set; }

        public UnitHealth _playerHealth = new(100, 100);

        void Awake()
        {
            if (gameManager != null && gameManager != this)
            {
                Destroy(this);
            }
            else
            {
                gameManager = this;
            }
        }
    }
}
