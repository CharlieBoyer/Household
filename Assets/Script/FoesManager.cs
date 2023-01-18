using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Script
{
    public class FoesManager : MonoBehaviour
    {
        public GameObject foePrefab;
        private Home _home;
        private bool _gameOver;

        private static readonly float _intervalTime = 4f;
        private readonly WaitForSeconds _spawnInterval = new WaitForSeconds(_intervalTime);

        public int foeSpeed;

        void Start()
        {
            _home = GameObject.Find("Home").GetComponent<Home>();
            _gameOver = false;
            foePrefab.GetComponent<FoesController>().speed = foeSpeed;

            StartCoroutine(SpawnFoe());
        }

        private void FixedUpdate()
        {
            _gameOver = _home.isDetroyed();
            if (_gameOver)
            {
                StopCoroutine(SpawnFoe());
            }
        }

        private IEnumerator SpawnFoe()
        {
            Vector3 spawnLocation = new Vector3();

            while (!_gameOver)
            {
                int side = Random.Range(0, 4);

                switch (side)
                {
                    case 0:
                        spawnLocation.x = 90;
                        spawnLocation.z = Random.Range(-90f, 90f);
                        break;
                    case 1:
                        spawnLocation.x = -90;
                        spawnLocation.z = Random.Range(-90f, 90f);;
                        break;
                    case 2:
                        spawnLocation.x = Random.Range(-90f, 90f);;
                        spawnLocation.z = 90;
                        break;
                    case 3:
                        spawnLocation.x = Random.Range(-90f, 90f);;
                        spawnLocation.z = -90;
                        break;

                }

                yield return _spawnInterval;
                GameObject foe = Instantiate(foePrefab, spawnLocation, Quaternion.Euler(Vector3.zero));
            }
        }
    }
}
