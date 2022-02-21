using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; //NameSpace

public class Movement : MonoBehaviour //Deriving from MonoBehaviour. Inheritance
{
    public Rigidbody rb; // Member variable.
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f; 
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }

    private void ProcessInput()
    {
        if ( Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }

    }

    private void ProcessRotation()
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

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing rotation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; 
    }
}