using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isControllable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isControllable)
        {
            return;
        }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("friend");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        isControllable = false;
        Invoke("ReloadLevel", delayTime);
    }

    private void StartSuccessSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayTime);
    }

    private void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        
        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
