using UnityEngine;

public class ClickablePart : MonoBehaviour
{
    public string partName; // ex: "BL", "BR", etc.
    public WordScriptManager manager;
    public AudioClip syllableClip; // 🔊 Le son propre à cette syllabe
    private AudioSource audioSource;

    private void Start()
    {
        // Prend l'AudioSource attachée à ce GameObject (obligatoire)
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogWarning("🔊 Aucun AudioSource trouvé sur " + gameObject.name);
        }
    }

    private void OnMouseDown()
    {
        manager.OnClickPart(partName);

        // 🔊 Joue le son de la syllabe si possible
        if (audioSource != null && syllableClip != null)
        {
            audioSource.PlayOneShot(syllableClip);
        }
    }
}
