using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; //NameSpace

public class Movement : MonoBehaviour //Deriving from MonoBehaviour. Inheritance
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;



    public Rigidbody rb; // Member variable.
    public AudioSource audioSource;
   

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticles.Stop();
    }

    private void StartThrusting()
    {
        if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngineSound);
        if (!mainThrustParticles.isPlaying) mainThrustParticles.Play();
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    private void ProcessRotation()
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
            StopRotating();
        }
    }

    private void StopRotating()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrustParticles.isPlaying) leftThrustParticles.Play();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrustParticles.isPlaying) rightThrustParticles.Play();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing rotation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; 
    }
}
