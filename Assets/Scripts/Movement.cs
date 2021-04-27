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

     [SerializeField] private AudioClip mainEngine;
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
            ApplyRotation(rotationThrust);
        }



        else if (Input.GetKey(KeyCode.D))
        {

            ApplyRotation(-rotationThrust);

        }
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
            audioSource.PlayOneShot(mainEngine);
        }
       

    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
             PlaySound();
        }
         else
        {
            audioSource.Stop();
        }

    }
}
