using UnityEngine;
using UnityEngine.UI;

public class WordScriptManager : MonoBehaviour
{
    public Text hintText; // Texte UI pour afficher les indices
    public AudioSource audioSource;
    public AudioClip[] hintClips; // Tableau d'audios : "Clique sur BL", "BR", etc.

    private string[] correctOrder = { "A", "AN", "ANAN", "ANANA", "ANANAS" }; // À adapter à tes syllabes
    private int currentStep = 0;

    void Start()
    {
        GiveHint(0); // Premier indice dès le lancement
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
                hintText.text = "🎉 Félicitations, tu as trouvé le mot complet !";
                PlayClip(currentStep);
            }
        }
        else
        {
            hintText.text = "Essaie encore !";
        }
    }

    void GiveHint(int step)
    {
        if (step < correctOrder.Length)
        {
            hintText.text = "👉 Clique sur \"" + correctOrder[step] + "\"";
            PlayClip(step);
        }
    }

    void PlayClip(int index)
    {
        if (hintClips != null && index >= 0 && index < hintClips.Length && hintClips[index] != null)
        {
            audioSource.PlayOneShot(hintClips[index]);
        }
        else
        {
            Debug.LogWarning("🎧 Aucun audio clip disponible pour l'étape " + index);
        }
    }
}
