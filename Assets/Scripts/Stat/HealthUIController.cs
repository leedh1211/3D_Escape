using System;
using UnityEngine;
using UnityEngine.UI;

namespace Stat
{
    public class HealthUIController : MonoBehaviour
    {
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private Text healthText;
        [SerializeField] private Image healthBar;

        private void Start()
        {
            healthSystem.OnHealthChanged += UpdateHealthUI;
            UpdateHealthUI(healthSystem.CurrentHealth, healthSystem.MaxHealth);
        }

        private void OnDisable()
        {
            healthSystem.OnHealthChanged -= UpdateHealthUI;
        }
        
        private float lastRatio = -1f;
        private void UpdateHealthUI(float currentHealth, float maxHealth)
        {
            float ratio = currentHealth / maxHealth;
            if (Mathf.Approximately(ratio, lastRatio)) return;

            healthBar.fillAmount = ratio;
            healthText.text = $"{Mathf.RoundToInt(ratio * 100)}%";
            lastRatio = ratio;
        }
    }
}