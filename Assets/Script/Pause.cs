using UnityEngine;

namespace Script
{
    public class Pause : MonoBehaviour
    {
        private Canvas _hud;
        private Canvas _runtimeMenu;
        private bool _isPaused;
        
        private void Start() 
        {
            _hud = GameObject.Find("HUD").GetComponent<Canvas>();
            _runtimeMenu = GameObject.Find("RuntimeMenu").GetComponent<Canvas>();
            _runtimeMenu.enabled = false;
            _isPaused = false;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel") && !_isPaused)
            {
                _isPaused = true;
                
                Time.timeScale = 0;
                _hud.enabled = false;
                _runtimeMenu.enabled = true;
                Cursor.visible = true;
            }
            else if (Input.GetButtonDown("Cancel") && _isPaused)
            {
                _isPaused = false;
                
                Time.timeScale = 1;
                _hud.enabled = true;
                _runtimeMenu.enabled = false;
                Cursor.visible = false;
            }
        }
    }
}

