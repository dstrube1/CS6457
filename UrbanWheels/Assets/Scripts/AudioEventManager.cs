using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioEventManager : MonoBehaviour
{
    public AudioSource pauseMenuMusic;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenuMusic = GetComponent<AudioSource>();
        if(SceneManager.GetActiveScene().name == "Opening Menu" || SceneManager.GetActiveScene().name == "End of Day") {
            pauseMenuMusic.Play();
        }
        else {
            pauseMenuMusic.Stop();
        }
    }


    public AudioClip crashAudio; // Sound to play when the car collides with an object

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPauseMenuMusic(){
        pauseMenuMusic.Play();
    }
    public void StopPauseMenuMusic(){
        pauseMenuMusic.Stop();
    }

    public void playCollision() {
        pauseMenuMusic.PlayOneShot(crashAudio);
    }
}
