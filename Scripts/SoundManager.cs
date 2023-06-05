using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip[] soundEffects;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(int index)
    {
        if (index < 0 || index >= soundEffects.Length)
        {
            Debug.LogError("Invalid sound effect index!");
            return;
        }

        audioSource.PlayOneShot(soundEffects[index]);
    }
}
