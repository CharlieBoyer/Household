using System.Collections;
using UnityEngine;

using Managers;

namespace Foes
{
    public abstract class Foe : MonoBehaviour
    {
        public int maxHealth;
        public float movementSpeed;
        
        [Header("Attack values")]
        public float attackPreparationDelay;
        public float attackSpeed;
        public int attackDamages;

        private bool _isAlive;
        private bool _isAttacking;
        private float _currentHealth;

        protected virtual void Start()
        {
            _currentHealth = maxHealth;
            _isAlive = true;
            _isAttacking = false;
        }

        protected void OnCollisionEnter(Collision collision)
        {
            string colliderObject = collision.gameObject.tag;

            switch (colliderObject)
            {
                case "Home" when _isAttacking == false:
                    StartCoroutine(StartAttacking());
                    break;
                case "Ammo":
                    TakeDamage(/*obj.gameObject.GetComponent<Ammo>().damage*/);
                    break;
                default:
                    return;
            }
        }

        protected virtual IEnumerator StartAttacking()
        {
            _isAttacking = true;
            yield return new WaitForSeconds(attackPreparationDelay);

            while (_isAlive) {
                Attack();
                yield return new WaitForSeconds(attackSpeed);
            }
        }

        protected virtual void Attack()
        {
            HomeManager.Instance.TakeDamage(attackDamages);
            // TODO: Attack animation
        }

        protected virtual void TakeDamage(int amount = 0)
        {
            if (amount == 0) {
                Debug.Log("Notice: NightshadeSpirit taking no damage");
                return;
            }

            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                // TODO: vanish animation
                // TODO: Count kills ?
                Destroy(this.gameObject);
            }
        }
    }
}
