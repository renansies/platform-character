using UnityEngine;
using UnityEngine.UI;

namespace Commons 
{
    public class HealthBar : MonoBehaviour
    {
        Slider _healthSlider;

        private void Start()
        {
            _healthSlider = GetComponent<Slider>();
        }

        public void SetMaxHealth(int maxHealth)
        {
            _healthSlider.maxValue = maxHealth;
            _healthSlider.value = maxHealth;
        }

        public void SetHealth(int health)
        {
            _healthSlider.value = health;
        }
    }
}
