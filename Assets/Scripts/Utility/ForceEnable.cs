using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class ForceEnable: MonoBehaviour
    {
        public bool activate;
        public List<GameObject> objectsToEnable;

        private void Start()
        {
            if (!activate)
                return;

            foreach (GameObject item in objectsToEnable)
                item.SetActive(true);
        }
    }
}
