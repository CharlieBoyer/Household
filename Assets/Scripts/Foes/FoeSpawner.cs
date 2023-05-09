using System.Collections.Generic;
using UnityEngine;

namespace Foes
{
    public class FoeSpawner: MonoBehaviour
    {
        private List<GameObject> _prefabs;

        public void SetPrefabs(List<GameObject> prefabs) {
            _prefabs = prefabs;
        }
    }
}
