using UnityEngine;

public class BlendSoundInteraction : MonoBehaviour
{
    public string partName;
    public WordHintManager manager;

    [Header("Audio Blending")]
    public AudioClip primaryClip;         // Ce clip est joué sur le premier clic
    public AudioClip blendedClip;         // Ce clip est joué ensuite (ex: AN après A)
    public float blendDuration = 1.0f;
    public float targetVolume = 1.0f;

    private AudioSource audioSource;
    private bool blending = false;
    private float blendTimer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = primaryClip;
        audioSource.volume = 1.0f;
    }

    private void OnMouseDown()
    {
        if (blending) return;

        // Appelle la logique de progression dans WordHintManager
        manager.OnClickPart(partName);

        // Joue le son principal
        if (primaryClip != null)
        {
            audioSource.clip = primaryClip;
            audioSource.volume = 1.0f;
            audioSource.Play();
        }

        // S'il y a un blendedClip défini, on lance la fusion après le premier son
        if (blendedClip != null)
        {
            blending = true;
            blendTimer = 0f;
        }
    }

    void Update()
    {
        if (blending)
        {
            blendTimer += Time.deltaTime;
            if (blendTimer >= blendDuration)
            {
                blending = false;
                audioSource.clip = blendedClip;
                audioSource.volume = targetVolume;
                audioSource.Play();
            }
        }
    }
}
