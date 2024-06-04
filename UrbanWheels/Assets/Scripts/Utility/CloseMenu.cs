using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseMenu : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Button saveButton;
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

    public void closeMenu() {
        canvasGroup = canvasGroup.GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        Time.timeScale = 1f;
        saveButton.interactable = true;
        pauseAudioManager.StopPauseMenuMusic();
        pauseButton.interactable = true;

    }
}
