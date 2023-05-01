using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> musicClips;
    public float volume = 0.5f;

    private AudioSource audioSource;
    private int currentClipIndex = -1;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        PlayRandomClip();
    }

    private void PlayRandomClip()
    {
        if (musicClips.Count == 0)
        {
            return;
        }

        int randomIndex = currentClipIndex;
        while (randomIndex == currentClipIndex)
        {
            randomIndex = Random.Range(0, musicClips.Count);
        }
        currentClipIndex = randomIndex;

        audioSource.clip = musicClips[currentClipIndex];
        audioSource.Play();
    }

    public void SkipToNextTrack()
    {
        if (musicClips.Count == 0)
        {
            return;
        }

        int nextIndex = (currentClipIndex + 1) % musicClips.Count;
        currentClipIndex = nextIndex;

        audioSource.clip = musicClips[currentClipIndex];
        audioSource.Play();
    }

    public string GetCurrentTrackName()
    {
        if (currentClipIndex == -1)
        {
            return "";
        }

        return musicClips[currentClipIndex].name;
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandomClip();
        }
    }
}
