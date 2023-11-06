using UnityEngine;
/// <summary>
/// This script defines the behavior of a coin in the game.
/// Coins can be collected by the player and provide points when collected.
/// </summary>
public class Coin : MonoBehaviour
{
    [SerializeField] private float CoinTurningSpeed; // Speed of coin rotation.

    // Called when another collider enters the trigger zone of the coin.
    private void OnTriggerEnter(Collider other)
    {
        // Checking if the coin is colliding with an obs.
        if (other.gameObject.GetComponent<Obs>() != null)
        {
            // If the coin collides with an obstacle, destroy the coin.
            Destroy(gameObject);
            return;
        }

       // Checking if the collided object is tagged as "Player".
       if (!other.gameObject.CompareTag("Player"))
           return; // Do nothing if the collided object is not the player.


        // Access the GameManager singleton instance and increase the player's score.
        GameManager.gameManager.CollideWithCoin();


        AudioHandler.audioHandler.PlayRandomCoinPickupSound();


        // Destroy this coin object after it's collected.
        Destroy(gameObject);
    }

    private void Update() => transform.Rotate(0, 0, CoinTurningSpeed * Time.deltaTime);
}