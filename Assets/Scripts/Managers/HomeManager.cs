using UnityEngine;

using Internal;

// ReSharper disable InconsistentNaming

namespace Managers
{
    public class HomeManager : MonoBehaviourSingleton<HomeManager>
    {
        [Header("Durability")]
        public int initialMaxDurability;

        private UIManager _ui;

        private int _maxDurability;
        private int _durability;
        private float _clampedDurability => (float)_durability/_maxDurability;

        private void Start()
        {
            _ui = UIManager.Instance;
            _maxDurability = initialMaxDurability;
        }

        // Durability Management
        public void TakeDamage(int amount) {
            _durability -= amount;
            _durability = Mathf.Clamp(_durability, 0, _maxDurability);
            // TODO: UI Update call
        }

        public void Repair(int amount)
        {
            _durability += amount;
            _durability = Mathf.Clamp(_durability, 0, _maxDurability);
            // TODO: UI Update call
        }

        public void IncreaseMaxDurability(int additionalMaxAmount)
        {
            float extraGaugeChunks = 0f;
            int maxAmountBasePercent = additionalMaxAmount / initialMaxDurability;

            extraGaugeChunks = maxAmountBasePercent switch
            {
                _ when maxAmountBasePercent > 0.25 => 0.5f,
                _ when maxAmountBasePercent > 0.5 => 1f,
                _ when maxAmountBasePercent > 1 => 2f,
                _ => 0
            };

            _maxDurability += additionalMaxAmount;
            _ui.homeStatus.IncreaseMaxGaugeSize(extraGaugeChunks);
        }
    }
}
