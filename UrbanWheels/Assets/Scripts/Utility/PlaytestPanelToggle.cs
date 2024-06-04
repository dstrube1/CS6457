using UnityEngine;

public class PlaytestPanelToggle : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Input.GetKeyUp() should eventually be replaced with Input.GetButtonUp() with a virtual button created in the InputManager settings
        //This will allow multiple game controllers to map to common input events (e.g. simultaneous keyboard, and handheld game controller support).
        if (Input.GetKeyUp (KeyCode.Escape)) {
            if (canvasGroup.interactable) {
                canvasGroup.interactable = false; 
                canvasGroup.blocksRaycasts = false; 
                canvasGroup.alpha = 0f;
                //When playtest panel is not visible, resume the game:
                Time.timeScale = 1f;
            } else {
                canvasGroup.interactable = true; 
                canvasGroup.blocksRaycasts = true; 
                canvasGroup.alpha = 1f;
                //When playtest panel is visible, pause the game:
                Time.timeScale = 0f;
            } 
        }

    }

    void Awake () {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component not found on " + gameObject.name);
        }
    }

}
