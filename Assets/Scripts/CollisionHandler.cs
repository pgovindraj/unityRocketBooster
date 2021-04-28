using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
   [SerializeField] private float levelLoadDelay =1f;
   [SerializeField] private AudioClip crashAudio;
   [SerializeField] private AudioClip successAudio;

   
   [SerializeField] private ParticleSystem crashParticles;
   [SerializeField] private ParticleSystem successParticles;




   AudioSource audioSource;

   bool isTransitioning=false;
   
   private void Start() {
       audioSource = GetComponent<AudioSource>();
   }

    private void OnCollisionEnter(Collision other) {

        if(isTransitioning){
            return;
        }

       switch(other.gameObject.tag){

           case "Friendly":
           Debug.Log(other.gameObject.tag);
           break;
           
           case "Fuel":
          
           break;
           
           case "Finish":
               StartSuccessSequence();
           break;

           default:
           StartCrashSequence();
           break;
       }

       
   }

    private void StartSuccessSequence()
    {
        isTransitioning=true;
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        successParticles.Play();
             GetComponent<Movement> ().enabled=false;
        Invoke("LoadNextLevel", levelLoadDelay);
        
    }

    void StartCrashSequence(){
        isTransitioning=true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        crashParticles.Play();
        GetComponent<Movement> ().enabled=false;
        Invoke("ReloadLevel", levelLoadDelay);
       

   }

    private void LoadNextLevel()
    {
       int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       int nextSceneIndex = currentSceneIndex +1; 
       if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
           nextSceneIndex=0;
       }
        SceneManager.LoadScene(nextSceneIndex);   
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);    
    }
}
