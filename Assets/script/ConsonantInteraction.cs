using UnityEngine;

namespace DyslexiaGame
{
    public class ConsonantInteraction : MonoBehaviour
    {
        private AudioSource audioSource;
        private MeshRenderer meshRenderer; // Composant utilisé pour le feedback visuel.
        private SpriteRenderer spriteRenderer; // Ajouté pour le changement de couleur des sprites.

        [Tooltip("Le clip audio à jouer lors de l'interaction avec la consonne.")]
        public AudioClip consonantSound;

        [Tooltip("La couleur à appliquer lors de l'interaction.")]
        public Color interactionColor = Color.yellow;

        private Color originalColor;

        void Start()
        {
            // Initialisation des composants
            audioSource = GetComponent<AudioSource>();
            meshRenderer = GetComponent<MeshRenderer>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (audioSource == null)
            {
                Debug.LogError("Composant AudioSource manquant sur " + gameObject.name);
            }

            if (meshRenderer != null)
            {
                originalColor = meshRenderer.material.color;
            }
            else if (spriteRenderer != null)
            {
                originalColor = spriteRenderer.color;
            }
            else
            {
                Debug.LogWarning("Ni MeshRenderer ni SpriteRenderer trouvés sur " + gameObject.name + ". Le feedback visuel sera désactivé.");
            }
        }

        /// <summary>
        /// Joue le son et active le feedback visuel.
        /// </summary>
        public void Interact()
        {
            PlayConsonantSound();
            ProvideVisualFeedback();
        }

        /// <summary>
        /// Joue le clip audio associé à la consonne.
        /// </summary>
        private void PlayConsonantSound()
        {
            if (audioSource != null && consonantSound != null)
            {
                audioSource.clip = consonantSound;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource ou ConsonantSound est nul sur " + gameObject.name);
            }
        }

        /// <summary>
        /// Change la couleur de l'objet pour indiquer l'interaction.
        /// </summary>
        private void ProvideVisualFeedback()
        {
            if (meshRenderer != null)
            {
                meshRenderer.material.color = interactionColor;
                Invoke("ResetColor", 0.2f); // Réinitialisation après un court délai.
            }
            else if (spriteRenderer != null)
            {
                spriteRenderer.color = interactionColor;
                Invoke("ResetColor", 0.2f);
            }
        }

        private void ResetColor()
        {
            if (meshRenderer != null)
            {
                meshRenderer.material.color = originalColor;
            }
            else if (spriteRenderer != null)
            {
                spriteRenderer.color = originalColor;
            }
        }

        // Interaction exemple avec clic (peut être adapté pour d'autres entrées comme touch ou clavier).
        void OnMouseDown()
        {
            Interact();
        }
    }
}
