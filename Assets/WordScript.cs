using UnityEngine;

public class WordScript : MonoBehaviour
{
    public string partName; 
    private AudioSource audioSource;
    public WordHintManager manager;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            manager.OnClickPart(partName);
        }
    }
}