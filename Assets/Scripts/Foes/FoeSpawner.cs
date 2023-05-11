using System.Collections.Generic;
using UnityEngine;

namespace Foes
{
    public class FoeSpawner: MonoBehaviour
    {
        public List<GameObject> prefabs;

        public void SetPrefabs(List<GameObject> foesPrefabs) {
            prefabs = foesPrefabs;
        }
    }
}
