using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpsAndSounds : MonoBehaviour
{
    private AudioSource audioSource;
    public ParticleSystem[] prticleSystem;
    public AudioClip[] clips_;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag != "Enemy")
        {
            Instantiate(prticleSystem[0], collision.GetContact(0).point, Quaternion.identity);
            audioSource.Stop();
            audioSource.clip = clips_[0];
            audioSource.Play();
        }
        else
        {
            //Instantiate(prticleSystem[0], collision.GetContact(0).point, Quaternion.identity);
        }
    }

    public void ShotSound()
    {
        audioSource.Stop();
        audioSource.clip = clips_[1];
        audioSource.Play();
    }

    public void spawnPrefab(int i, Vector3 point)
    {
        Instantiate(prticleSystem[i], point, Quaternion.identity);
    }
}
