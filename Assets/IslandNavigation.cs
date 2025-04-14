using UnityEngine;
using UnityEngine.SceneManagement;

public class IslandNavigation : MonoBehaviour
{
    // Nom de la scène suivante
    [SerializeField] private string nextSceneName;

    public void LoadNextIsland()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
