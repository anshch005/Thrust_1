using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetect : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] AudioClip CrashSFX;
    [SerializeField] AudioClip SuccessSFX;
    [SerializeField] ParticleSystem SuccessParticle;
    [SerializeField] ParticleSystem CrashParticle;
    AudioSource audioSource;
    bool isControllable=true;
    private void Start()
    {
        audioSource= GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) 
{  
    if(!isControllable)
        {
            return;
        }
    {
    switch(other.gameObject.tag)
        {
        case "Start":
            Debug.Log("Start pull the trigger");
            break;
        case "Finish":
            StartSuccessSequence();
            break;
        default:
            StartCrashSequence();
            break;
        }
    }
}
    void StartSuccessSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(SuccessSFX);
        SuccessParticle.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("LoadNextLevel",LevelLoadDelay);//minor delay before loading the level
    }
    void StartCrashSequence()
    {
        isControllable =  false; //disable handling in case of crash
        audioSource.Stop();
        audioSource.PlayOneShot(CrashSFX);
        CrashParticle.Play();
        GetComponent<Movement>().enabled=false; //turns off the movement during that window 
        Invoke("ReloadLevel",LevelLoadDelay); //invoke for delay 
    }
    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextscene =  currentScene + 1;

        if (nextscene == SceneManager.sceneCountInBuildSettings)
        {
            nextscene= 0;
        }
        SceneManager.LoadScene(nextscene);
    }
    void ReloadLevel()
    {

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}