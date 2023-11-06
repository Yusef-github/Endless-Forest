using UnityEngine;
public class Obs : MonoBehaviour
{
    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement.playerMovement.Die();
            PlayerMovement.playerMovement.alive = false;
        }
    }
}