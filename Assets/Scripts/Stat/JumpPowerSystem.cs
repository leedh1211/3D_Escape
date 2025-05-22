using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Stat
{
    public class JumpPowerSystem : MonoBehaviour
    {
        public float MaxJumpPower { get; set; }
        public float MinJumpPower { get; set; }
        public float CurrentJumpPower { get; private set; }

        public event Action<float, float, float> OnJumpPowerChanged;
        public event Action OnDamage;
        public event Action OnDeath;

        private float ChargeSpeed = 50;

        private void Awake()
        {
            MaxJumpPower = 200f;
            MinJumpPower = 80f;
            CurrentJumpPower = MinJumpPower;
        }

        public void ChargeJumpPower(float delta)
        {
            CurrentJumpPower = Mathf.Min(CurrentJumpPower + delta * ChargeSpeed, MaxJumpPower);
            OnJumpPowerChanged?.Invoke(CurrentJumpPower, MaxJumpPower, MinJumpPower);
        }

        public void ChangeDefaultJumpPower()
        {
            CurrentJumpPower = MinJumpPower;
            OnJumpPowerChanged?.Invoke(CurrentJumpPower, MaxJumpPower, MinJumpPower);
        }

        public void SetChargeSpeed(float itemJumpChargeSpeed)
        {
            ChargeSpeed = itemJumpChargeSpeed;
        }

        public void ResetChargeSpeed()
        {
            ChargeSpeed = 50f;
        }
    }
}
