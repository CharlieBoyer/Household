using System;
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
            _durability = _maxDurability;
            _ui.homeStatus.UpdateDurability(_clampedDurability, true);
        }

        public void TakeDamage(int amount) {
            _durability -= amount;
            _durability = Mathf.Clamp(_durability, 0, _maxDurability);
            _ui.homeStatus.UpdateDurability(_clampedDurability, false);
            _ui.UpdateGameInfo("Under Attack !");
        }

        public void Repair(int amount)
        {
            _durability += amount;
            _durability = Mathf.Clamp(_durability, 0, _maxDurability);
            _ui.homeStatus.UpdateDurability(_clampedDurability, false);
        }

        public void IncreaseMaxDurability(int additionalMaxAmount)
        {
            float extraGaugeChunks;
            float maxAmountBasePercent = (float) additionalMaxAmount / initialMaxDurability;
            
            switch (maxAmountBasePercent)
            {
                case <= 0.5f:
                    extraGaugeChunks = 0.5f;
                    break;
                case > 0.5f and < 1f:
                    extraGaugeChunks = 1;
                    break;
                case >= 1f and < 2.5f:
                    extraGaugeChunks = 2;
                    break;
                default:
                    extraGaugeChunks = 4;
                    break;
            }

            _maxDurability += additionalMaxAmount;
            Repair(additionalMaxAmount);
            _ui.homeStatus.IncreaseMaxGaugeSize(extraGaugeChunks);
        }
    }
}
