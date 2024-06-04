using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CarCollider : MonoBehaviour

{

    public AudioEventManager audioManager;
    public GameObject ragdollboy;
    private Animator boyAnimator;
    public GameManager gameManager;
    private bool canTakeDamage = true;

    private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Boy"))
            {
                Debug.Log("Collision with the target prefab detected!");
                audioManager.playCollision();
                //get the position of the boy we hit
                Vector3 boyPosition = collision.gameObject.transform.position;
                //create a ragdoll boy at that position
                GameObject instantitatedBoy = Instantiate(ragdollboy, boyPosition, Quaternion.identity);
                //
                boyAnimator = instantitatedBoy.GetComponent<Animator>();
                //turn the animator off when the boy collides with the car, starting the ragdoll boy 
                boyAnimator.enabled = false;
                //destroy the original boy
                Destroy(collision.gameObject);
                if (canTakeDamage) {
                    if (gameManager.health - 10 <= 0) {
                        gameManager.health = 0;
                    }
                    else {
                        gameManager.health -= 10;
                    }
                    canTakeDamage = false;
                    StartCoroutine(ResetDamageTimer());
                    if (gameManager.health == 0) {
                        PlayerPrefs.SetInt("dayCount", gameManager.GetComponent<GameManager>().dayCount);
                        PlayerPrefs.SetFloat("Earnings", gameManager.GetComponent<GameManager>().earnings);
                        PlayerPrefs.SetInt("totalTrips", gameManager.GetComponent<GameManager>().totalTrips);
                        PlayerPrefs.SetFloat("overallRating", gameManager.GetComponent<GameManager>().overallRating);
                        PlayerPrefs.Save();
                        SceneManager.LoadScene("End of Day");
                    }
                }
                // audioSource.PlayOneShot(collisionSound);
                // Perform actions specific to the collision with the target prefab
            }
        }
    IEnumerator ResetDamageTimer() {
        yield return new WaitForSeconds(.3f);
        canTakeDamage = true;
    }
}
