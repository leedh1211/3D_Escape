using UnityEngine;
using UnityEngine.UI;

namespace Stat
{
    public class JumpPowerUIController : MonoBehaviour
    {
        [SerializeField] private JumpPowerSystem _jumpPowerSystem;
        [SerializeField] private Image JumpBar;

        private void OnEnable()
        {
            _jumpPowerSystem.OnJumpPowerChanged += UpdateJumpUI;
            UpdateJumpUI(_jumpPowerSystem.CurrentJumpPower, _jumpPowerSystem.MaxJumpPower, _jumpPowerSystem.MinJumpPower);
        }

        private void OnDisable()
        {
            _jumpPowerSystem.OnJumpPowerChanged -= UpdateJumpUI;
        }

        private void UpdateJumpUI(float currentJumpPower, float maxJumpPower, float minJumpPower)
        {
            JumpBar.fillAmount = (currentJumpPower-minJumpPower) / (maxJumpPower - minJumpPower);
        }
    }
}