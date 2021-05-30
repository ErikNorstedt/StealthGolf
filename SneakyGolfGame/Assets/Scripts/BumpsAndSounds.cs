using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpsAndSounds : MonoBehaviour
{
    private AudioSource audioSource;
    //public ParticleSystem prticleSystem;
    public AudioClip[] clips_;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag != "Enemy")
        {
            //Instantiate(prticleSystem, collision.GetContact(0).point, Quaternion.identity);
            audioSource.Stop();
            audioSource.clip = clips_[0];
            audioSource.Play();
        }
    }

    public void ShotSound()
    {
        audioSource.Stop();
        audioSource.clip = clips_[1];
        audioSource.Play();
    }
}
