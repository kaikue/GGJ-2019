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
        int clipIndex = Random.Range(0, gunShots.Length);
        a.PlayOneShot(gunShots[clipIndex]);
    }

    public void PlayInjury()
    {
        AudioSource a = GetComponent<AudioSource>();
        int clipIndex = Random.Range(0, injuries.Length);
        a.PlayOneShot(injuries[clipIndex]);
    }

    public void PlayDeath()
    {
        AudioSource a = GetComponent<AudioSource>();
        int clipIndex = Random.Range(0, deaths.Length);
        a.PlayOneShot(deaths[clipIndex]);
    }
}
