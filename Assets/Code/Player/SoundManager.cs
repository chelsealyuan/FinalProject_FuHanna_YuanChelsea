using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //outlets
    AudioSource audioSource;
    public AudioClip chestSound;
   //blic AudioClip walkSound;
    public AudioClip unlockSound;
    public AudioClip lockedSound;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayChestSound()
    {
        audioSource.PlayOneShot(chestSound);
    }
    public void PlayUnlockSound()
    {
        audioSource.PlayOneShot(unlockSound);
    }


    public void PlayLockedSound()
    {
        audioSource.PlayOneShot(lockedSound);
    }


}
