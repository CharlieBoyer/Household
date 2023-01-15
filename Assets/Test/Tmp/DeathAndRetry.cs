using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathAndRetry : MonoBehaviour
{
    private Transform _player;
    void Start()
    {
        _player = transform;
    }

    void Update()
    {
        if (_player.position.y <= -10) // Si le joueur tombe plus bas que "-10"
        {
            Destroy(this.gameObject); // On détruit le joueur
        }
    }

    private void OnDestroy() // Quand le joueur est détruit
    {
        SceneManager.LoadScene("nomDeLaScene", LoadSceneMode.Single); // On restart le niveau (comme si on appuit sur "play")
    }
}
