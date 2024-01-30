using UnityEngine;

namespace Commons 
{
    public class PlayerBehaviour : MonoBehaviour
    {

        [SerializeField] HealthBar _healthbar;

        void Start()
        {
            
        }

        void Update()
        {
            
        }

        private void PlayerTakeDamage(int dmg)
        {
            GameManager.gameManager._playerHealth.DamageUnit(dmg);
            _healthbar.SetHealth(GameManager.gameManager._playerHealth.Health);
        }

        private void PlayerHeal(int healing)
        {
            GameManager.gameManager._playerHealth.HealUnit(healing);
            _healthbar.SetHealth(GameManager.gameManager._playerHealth.Health);
        }
    }
}
