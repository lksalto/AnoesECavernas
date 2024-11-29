using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> arrowSounds = new List<AudioClip>();
    public List<AudioClip> cannonSounds = new List<AudioClip>();
    public List<AudioClip> iceSounds = new List<AudioClip>();
    public List<AudioClip> playerDamage = new List<AudioClip>();
    private AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found! Please attach an AudioSource component to the GameObject.");
        }
    }

    public void Play(string sound)
    {
        if (audioSource == null) return; // Prevent errors if no AudioSource is found

        AudioClip clip = null;

        // Select a random sound based on the input
        if (sound == "Arrow" && arrowSounds.Count > 0)
        {
            clip = arrowSounds[Random.Range(0, arrowSounds.Count)];
        }
        else if (sound == "Cannon" && cannonSounds.Count > 0)
        {
            clip = cannonSounds[Random.Range(0, cannonSounds.Count)];
        }
        else if (sound == "Ice" && iceSounds.Count > 0)
        {
            clip = iceSounds[Random.Range(0, iceSounds.Count)];
        }
        else if (sound == "PlayerDamage" && playerDamage.Count > 0)
        {
            clip = playerDamage[Random.Range(0, iceSounds.Count)];
        }

        // Play the selected sound
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"No sound available for {sound}.");
        }
    }
}
