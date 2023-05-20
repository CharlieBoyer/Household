using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

using Wave;
using Utility;

namespace Foes
{
    public class FoeSpawner: MonoBehaviour
    {
        public GameObject container;
        
        private List<Collider> _areas;
        private WaveObject _nextWave;
        
        // Cached property
        private WaitForSeconds _safeguardAI;

        private void Awake()
        {
            _areas = new List<Collider>();

            foreach (Collider child in GetComponentsInChildren<Collider>())
            {
                if (child == null)
                    Debug.LogError("Error: GetComponetsInChildren() finds nothing");
                _areas.Add(child);
            }
            
            if (_areas.Count == 0)
                throw new Exception("FoeSpawner > Unable to get spawn areas");
        }

        public void SpawnWave(WaveObject wave)
        {
            if (wave == null)
                throw new Exception("FoeSpawner > Encounter a null WaveObject");

            _nextWave = wave;
            _safeguardAI = new WaitForSeconds(wave.spawnRate / 10);
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            List<GameObject> foesTypes = _nextWave.foesTypes;
            List<int> foesNumbers = _nextWave.foesNumbers;

            List<int> spawnIndices = new List<int>();
            for (int i = 0; i < foesTypes.Count; i++)
            {
                spawnIndices.AddRange(Enumerable.Repeat(i, foesNumbers[i]));
            }
            spawnIndices.Shuffle(); // Shuffle the indices to randomize the spawn order

            foreach (int index in spawnIndices)
            {
                GameObject foePrefab = foesTypes[index];
                GameObject newFoe = Instantiate(foePrefab, container.transform);

                newFoe.transform.position = GetSpawnPosition();
                newFoe.GetComponent<FoeController>().StartAI();
                
                yield return new WaitForSeconds(_nextWave.spawnRate);
            }
        }
    
        private Vector3 GetSpawnPosition()
        {
            Collider randomArea = _areas[Random.Range(0, _areas.Count)];

            if (randomArea == null)
                throw new Exception("FoeSpawner > A spawn area doesn't have a proper Collider");

            Bounds areaBounds = randomArea.bounds;
            Vector3 randomPosition = new Vector3(
                Random.Range(areaBounds.min.x, areaBounds.max.x),
                0f, // Set the initial Y position to 0
                Random.Range(areaBounds.min.z, areaBounds.max.z)
            );

            // Adjust the Y position based on the terrain height
            Terrain terrain = GetTerrainAtPosition(randomPosition);
            if (terrain != null)
                randomPosition.y = terrain.SampleHeight(randomPosition) + terrain.transform.position.y;

            return randomPosition;
        }

        private Terrain GetTerrainAtPosition(Vector3 position)
        {
            if (Physics.Raycast(position + Vector3.up * 100f, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            {
                return hit.collider.gameObject.GetComponent<Terrain>();
            }

            return null;
        }
    }
}
