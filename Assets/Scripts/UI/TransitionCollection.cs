using System;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Serialization;

namespace UI
{
    [Serializable]
    public class TransitionCollection : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _transitions;
        private List<string> _names = new List<string>();

        private Dictionary<string, GameObject> _table = new Dictionary<string, GameObject>();

        private void Start()
        {
            for (int index = 0; index < _transitions.Count; index++)
            {
                _names.Add(_transitions[index].name);
                _table.Add(_names[index], _transitions[index]);
            }
        }

        public GameObject Get(string transitionName)
        {
            return _table[transitionName];
        }
    }
}
