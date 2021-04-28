using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    AudioSource audioSource;
    Rigidbody rb;
    // Start is called before the first frame update
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;

    [SerializeField] private ParticleSystem rightThrusterParticles;
    [SerializeField] private ParticleSystem leftThrusterParticles;

    [SerializeField] private ParticleSystem mainThrusterParticles;

    [SerializeField] private AudioClip mainEngineAudio;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessRotation()
    {

        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }



        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();

        }
        else
        {

            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();

        }
    }

    private void RotateRight()
    {
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }

        ApplyRotation(-rotationThrust);
    }

    private void RotateLeft()
    {
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }

        ApplyRotation(rotationThrust);
    }

    private void ApplyRotation(float rotateThisFrame)
    {

        rb.freezeRotation = true; // Freezing rotation so that it can be done manually
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //Unfreezing so that physics system can take over
    }

    private void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineAudio);
        }


    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            mainThrush();
        }

        else
        {
            audioSource.Stop();
            mainThrusterParticles.Stop();
        }

    }

    private void mainThrush()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        PlaySound();

        if (!mainThrusterParticles.isPlaying)
        {
            mainThrusterParticles.Play();
        }
    }
}
