using System.Collections.Generic;
using UnityEngine;

using Internal;
using Foes;

namespace Managers
{
    public class FoeManager: MonoBehaviourSingleton<FoeManager>
    {
        public List<GameObject> spawnAreas;
        public List<GameObject> foes; 

        private void Start()
        {
            foreach (GameObject area in spawnAreas)
            {
                area.GetComponent<FoeSpawner>().SetPrefabs(foes);
            }
        }
    }
}
