using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   [SerializeField] float levelLoadDelay= 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip finishSound;

    public AudioSource audioSource;
    bool isTransitioning = false;

    private void Start()
    {
        isTransitioning = false; 
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTransitioning)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Friendly fire");
                    break;
                case "Finish":
                    Debug.Log("Congrats!");
                    StartFinishSequence();
                    break;
                default:
                    Debug.Log("Sorry you blew up");
                    StartCrashSequence();
                    break;
            }
        }
    }

    private void StartFinishSequence()
    {   
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (SceneManager.sceneCountInBuildSettings == nextSceneIndex) nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
