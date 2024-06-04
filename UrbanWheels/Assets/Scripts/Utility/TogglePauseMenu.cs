using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// [RequireComponent(typeof(CanvasGroup))]
public class TogglePauseMenu : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public AudioEventManager pauseAudioManager;
    public Button pauseButton;
    
    // Start is called before the first frame update
    void Start()
    {
        pauseAudioManager = FindObjectOfType<AudioEventManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void closeMenu() {
    //     canvasGroup.interactable = false;
    //     canvasGroup.blocksRaycasts = false;
    //     canvasGroup.alpha = 0f;
    //     Time.timeScale = 1f;
    //     audioManager.StopPauseMenuMusic();
    // }

    public void openMenu() {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        Time.timeScale = 0f; 
        pauseAudioManager.StartPauseMenuMusic();
        pauseButton.interactable = false;
    }
}
