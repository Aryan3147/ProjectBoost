using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollissionHandler : MonoBehaviour
{
    [SerializeField] float LvlLoadDelay = 2f;
    [SerializeField] AudioClip Success;
    [SerializeField] AudioClip Death;

    [SerializeField] ParticleSystem SuccessParticle;
    [SerializeField] ParticleSystem DeathParticle;

    AudioSource audioSource;

    bool isTransitioning = false; 
    bool CollissionDisable = false;  

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

  /*  void Update()
    {
        RespondToDebugKeys();
    }

   void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLvl();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            CollissionDisable = ! CollissionDisable;
        }
    }*/
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || CollissionDisable)
        {
            return;
        }

        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                Debug.Log("Congrats you Finished");
                
                break;
            default:
                Debug.Log("oops! You blew up");
                StartCrashSequence();
                
                break;
        }   
    }

    void StartSuccessSequence()
    {
        isTransitioning = true ;
        audioSource.Stop();
        audioSource.PlayOneShot(Success);
        SuccessParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLvl" ,LvlLoadDelay);
    }

    void StartCrashSequence()
    { 
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Death);
        DeathParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("Reload" , LvlLoadDelay);
    }

    void Reload()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        int NextScene = CurrentScene + 1;
        if(NextScene == SceneManager.sceneCountInBuildSettings)
        {
            NextScene = 0;
        }

        SceneManager.LoadScene(CurrentScene);
        
    }

    void LoadNextLvl()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene + 1);       
    }
}
