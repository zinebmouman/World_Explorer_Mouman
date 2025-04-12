using UnityEngine;

public class VowelClick : MonoBehaviour
{
    // Référence au composant AudioSource (assigné dans Unity)
    public AudioSource vowelSound;
    public string vowel; // A, E, I, O, U (à définir dans l’inspecteur)

    // Référence au SpriteRenderer pour changer la couleur
    private SpriteRenderer spriteRenderer;
    public System.Action<string> OnVowelClicked;

    // Sauvegarder la couleur originale
    private Color originalColor;

    void Start()
    {
        // Récupère le SpriteRenderer de l'objet
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color; // Stocker la couleur originale
        }

        // Vérifie que le composant AudioSource est bien assigné
        if (vowelSound == null)
        {
            Debug.LogWarning("Aucun AudioSource assigné au script VowelClick sur " + gameObject.name);
        }
    }

    // Appelé automatiquement quand l'utilisateur clique sur l'objet
    void OnMouseDown()
    {
        // Joue le son si assigné
        if (vowelSound != null)
        {
            vowelSound.Play();
        }

        // Change la couleur temporairement pour un effet visuel
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            Invoke("ResetColor", 0.3f);

        }
        if (OnVowelClicked != null)
        {
            OnVowelClicked(vowel);
        }
        // Remettre la couleur originale

    }
    void ResetColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
}