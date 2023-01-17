using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class Home : MonoBehaviour
    {
        private int _hp;

        private void Awake()
        {
            _hp = 100;
        }

        private void FixedUpdate()
        {
            if (this.isDetroyed())
            {
                // SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Foe"))
            {
                _hp -= 10;
                Debug.Log("Remaining HP: " + _hp);
                Destroy(collision.gameObject);
            }
        }

        public bool isDetroyed()
        {
            return _hp <= 0;
        }
    }
}
