using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VowelGameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI vowelDisplayText;
    public Button hintButton;

    [Header("Vowel Buttons")]
    public List<VowelClick> vowelButtons; // Liste de tous les boutons de voyelles

    private List<string> vowels = new List<string> { "A", "E", "I", "O", "U" };
    private int currentVowelIndex = 0;

    private Color originalColor;
    private string originalText;

    void Start()
    {
        originalColor = vowelDisplayText.color;

        // Ajouter listener au bouton Hint
        if (hintButton != null)
            hintButton.onClick.AddListener(ShowHint);

        // Initialiser les voyelles
        foreach (var vb in vowelButtons)
        {
            vb.OnVowelClicked = OnVowelClicked;
        }

        SetCurrentVowel();
    }

    void SetCurrentVowel()
    {
        if (currentVowelIndex < vowels.Count)
        {
            originalText = "Find the vowel: " + vowels[currentVowelIndex];
            vowelDisplayText.text = originalText;
            vowelDisplayText.color = originalColor;
        }
        else
        {
            vowelDisplayText.text = "Game Completed!";
        }
    }

    public void ShowHint()
    {
        StopAllCoroutines();
        StartCoroutine(HintCoroutine());
    }

    IEnumerator HintCoroutine()
    {
        if (currentVowelIndex >= vowels.Count) yield break;

        vowelDisplayText.text = "Hint: It's " + vowels[currentVowelIndex];
        vowelDisplayText.color = Color.yellow;

        yield return new WaitForSeconds(3f);

        vowelDisplayText.text = originalText;
        vowelDisplayText.color = originalColor;
    }

    public void OnVowelClicked(string clickedVowel)
    {
        Debug.Log("Clicked Vowel: " + clickedVowel + ", Expected: " + vowels[currentVowelIndex]);

        if (currentVowelIndex >= vowels.Count) return;

        string expected = vowels[currentVowelIndex];

        if (clickedVowel.ToUpper() == expected)
        {
            vowelDisplayText.text = "Correct! It was " + expected;
            vowelDisplayText.color = Color.green;
            StartCoroutine(NextVowelDelay());
        }
        else
        {
            vowelDisplayText.text = "Try Again!";
            vowelDisplayText.color = Color.red;
        }
    }


    IEnumerator NextVowelDelay()
    {
        yield return new WaitForSeconds(2f);
        currentVowelIndex++;
        SetCurrentVowel();
    }
}
