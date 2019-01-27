using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAudio : MonoBehaviour
{
    public AudioClip[] gunShots;
    public AudioClip[] injuries;
    public AudioClip[] deaths;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGunshot()
    {
        AudioSource a = GetComponent<AudioSource>();
        //TODO vary IAW http://andrewmushel.com/sound-effect-variation-in-unity/
        a.PlayOneShot(gunShots[0]);
    }

    public void PlayInjury()
    {
        AudioSource a = GetComponent<AudioSource>();
        //TODO vary IAW http://andrewmushel.com/sound-effect-variation-in-unity/
        a.PlayOneShot(injuries[0]);
    }

    public void PlayDeath()
    {
        AudioSource a = GetComponent<AudioSource>();
        //TODO vary IAW http://andrewmushel.com/sound-effect-variation-in-unity/
        a.PlayOneShot(deaths[0]);
    }
}
