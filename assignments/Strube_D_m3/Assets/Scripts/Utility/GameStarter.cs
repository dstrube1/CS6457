// Purpose: Start the game when the start button is pressed.
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Debug.Log("StartGame has been called...");
        SceneManager.LoadScene ("demo");
        //Time.timeScale is preserved after loading a new level. 
        //So, if you paused in one scene then call SceneManager.LoadScene(), you will find the newly loaded scene to still be paused; unless...
        Time.timeScale = 1f;
        Debug.Log("Game Started");
    }
}
