using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Stat
{
    public class HealthSystem : MonoBehaviour
    {
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        public event Action<float, float> OnHealthChanged;
        public event Action OnDamage;
        public event Action OnDeath;

        private void Awake()
        {
            MaxHealth = 100f;
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth = Mathf.Max(CurrentHealth-damage,0);
            OnDamage?.Invoke();
            OnHealthChanged?.Invoke(CurrentHealth,MaxHealth);

            if (CurrentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        public void Heal(float heal)
        {
            CurrentHealth = Mathf.Min(CurrentHealth + heal,MaxHealth);
            OnHealthChanged?.Invoke(CurrentHealth,MaxHealth);
        }
    }
}
