using System.Collections.Generic;
using Environment;
using UnityEngine;

namespace Wave
{
    [CreateAssetMenu(fileName = "New Wave", menuName = "Custom/Wave")]
    public class WaveObject: ScriptableObject
    {
        [Header("Wave type")]
        public int duration;
        public Weather weather;

        [Header("Wave Composition")]
        public List<GameObject> foesTypes;
        public List<int> foesNumbers;

        [Header("Modifiers")]
        public float spawnRate;
        public bool addModifier;
        public WaveModifier modifier;
    }
}
