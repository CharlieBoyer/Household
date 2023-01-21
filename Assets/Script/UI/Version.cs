using UnityEngine;
using TMPro;

namespace Script.UI
{
    public class Version : MonoBehaviour
    {
        public TMP_Text versionText;

        private void Update()
        {
            versionText.text = "v " + Application.version;
        }
    }
}
