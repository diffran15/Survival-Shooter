﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    void Awake ()
    {
        //ambil komponen"
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }

    void Update ()
    {
        //kalo sinking, object pindah kebawah
        if (isSinking)
        {
            transform.Translate (sinkSpeed * Time.deltaTime * -Vector3.up);
        }
    }

    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death ();
        }
    }

    void Death ()
    {
        isDead = true;

        BuffPlayer();
        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }

    public void StartSinking ()
    {
        //disable komponen navmesh
        GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;

        //ubah rb ke kinematic
        GetComponent<Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }

    void BuffPlayer()
    {
        float random = Random.value;
        if (random > 0.95) //rate 5%
        {
            PlayerMovement.Instance.AddSpeed(1);
        }
        else if (random > 0.8) //rate 20%
        {
            PlayerHealth.Instance.AddHealth(5);
        }
        else if (random > 0.75) //rate 25%
        {
            PlayerShooting.Instance.IncreaseShooting(20, 0.01f);
        }    
    }
}
