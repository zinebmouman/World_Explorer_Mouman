using UnityEngine;
using UnityEngine.UI;

public class WordHintManager : MonoBehaviour
{
    public Text hintText; // Texte UI pour afficher les indices
    public AudioSource audioSource;
    public AudioClip[] hintClips; // Clips audio correspondant à chaque étape

    // Nouveau mot composé de syllabes "BL", "BR", etc.
    private string[] correctOrder = { "BL", "BR", "OQ", "CL" };
    private int currentStep = 0;

    void Start()
    {
        GiveHint(0); // Donne le premier indice dès le début
    }

    // Appelé par chaque syllabe cliquée
    public void OnClickPart(string part)
    {
        if (part == correctOrder[currentStep])
        {
            if (currentStep < correctOrder.Length - 1)
            {
                currentStep++;
                GiveHint(currentStep);
            }
            else
            {
                hintText.text = "🎉 Félicitations, tu est un champions !";
                if (currentStep < hintClips.Length)
                    audioSource.PlayOneShot(hintClips[currentStep]);
            }
        }
        else
        {
            hintText.text = "Essaie encore !";
        }
    }

    void GiveHint(int step)
    {
        string nextHint = "👉 Clique sur \"" + correctOrder[step] + "\"";
        hintText.text = nextHint;

        if (step < hintClips.Length && hintClips[step] != null)
        {
            audioSource.PlayOneShot(hintClips[step]);
        }
    }
}
