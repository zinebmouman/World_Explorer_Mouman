using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public Text scoreText;
    public Button hintButton;

    private int score = 0;

    void Start()
    {
        // Afficher le score initial
        UpdateScoreText();

        // Ajouter un listener pour le bouton d'indice
        hintButton.onClick.AddListener(ShowHint);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void ShowHint()
    {
        // À personnaliser selon ton jeu
        Debug.Log("Hint shown!");
    }
}
