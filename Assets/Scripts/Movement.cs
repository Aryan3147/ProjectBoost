using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]float mainThrust = 100f;
    [SerializeField]float rotationThrust = 1f;
    [SerializeField]AudioClip mainEngine;
    Rigidbody rigid;
    AudioSource audiosource ;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem rightEngineParticle;
    [SerializeField] ParticleSystem leftEngineParticle;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
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



    void StartThrusting()
    {
        rigid.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticle.isPlaying)
        {
            mainEngineParticle.Play();
        }
    }
    void StopThrusting()
    {
        audiosource.Stop();
        mainEngineParticle.Stop();
    }



    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }



    void RotateRight()
    {
        RotateDirection(-rotationThrust);

        if (!leftEngineParticle.isPlaying)
        {
            leftEngineParticle.Play();
        }
    }

    void RotateLeft()
    {
        RotateDirection(rotationThrust);

        if (!rightEngineParticle.isPlaying)
        {
            rightEngineParticle.Play();
        }
    }
    void StopRotating()
    {
        rightEngineParticle.Stop();
        leftEngineParticle.Stop();
    }

    void RotateDirection(float rotateThisFrame)
    {
        rigid.freezeRotation = true ; // freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.deltaTime);
        rigid.freezeRotation = false ; // unfreeze rotation so physics can take over
    }

}
