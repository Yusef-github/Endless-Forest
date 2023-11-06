using System.Collections;
using UnityEngine;
/// <summary>
/// Handles player movement and input, including jumping and swiping.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    // Movement
    public float speed;
    private float sensitivity = 1;
    private float horizontalInput;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float horizontalMultiplier;

    // Jump
    public float jumpForce;
    private bool canJump = true;
    [SerializeField] private AudioSource JumpAudio;


    // UI Elements to Activate/Deactivate.
    [SerializeField] private GameObject LoosePanel, PauseButton;


    // Singleton instance, providing global access to this script functionality.
    public static PlayerMovement playerMovement;

    // On Loosing
    [HideInInspector] public bool alive = false, FellDown;
    public Animator CharacterAnimator;


    private Vector2 touchStartPos;

    private void Start() => playerMovement = this;


    // Handles the player's movement in the game world during a fixed time step.
    private void FixedUpdate()
    {
        if (!alive) return;

        // Calculate the forward and horizontal movement based on player input and sensitivity.
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier * sensitivity;

        // Move the player's Rigidbody to the new position by adding forward and horizontal movement.
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }


    private void Update()
    {
        if(alive == true)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchStartPos = touch.position;
                        break;

                    case TouchPhase.Moved:
                        Vector2 deltaSwipe = touch.position - touchStartPos;

                        // Determine whether the player is swiping horizontally or vertically.
                        if (Mathf.Abs(deltaSwipe.x) > Mathf.Abs(deltaSwipe.y))
                            horizontalInput = deltaSwipe.x > 0 ? 1 : -1;// Set horizontal input based on swipe direction (1 for right, -1 for left).

                        else
                        {
                            // If swiping vertically, check for a jump and set horizontal input accordingly.
                            if (canJump && deltaSwipe.y > 0)
                                Jump();

                            horizontalInput = 0;
                        }
                        break;

                    case TouchPhase.Ended:
                        // Reset swipe-related values when the touch ends.
                        touchStartPos = Vector2.zero;
                        horizontalInput = 0;
                        break;
                }
            }
            else
                horizontalInput = 0;


            if (transform.position.y < -1 && FellDown == true)
            {
                FellDown = false;
                Die();
            }
        }
    }

    // Makes the player jump by applying an upward force and prevents multiple jumps until the player lands.
    void Jump()
    {
        if (AudioHandler.audioHandler.isAudioEnabled == true)
            JumpAudio.Play();

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        canJump = false;
    }


    // Sets the sensitivity of player movement.
    public void SetSensitivity(float value) => sensitivity = value;


    // Handles the player's death by displaying UI elements, stopping the game, and showing interstitial ads.
    public void Die()
    {
        Interstitial.interstitial.LoadInterstitial();
        CharacterAnimator.enabled = false;
        StartCoroutine(DelayedActions());
    }

    private IEnumerator DelayedActions()
    {
        //I added this because sometimes the ad is showing directly, sometimes it is delayed.
        yield return new WaitForSeconds(2f);

        alive = false;
        LoosePanel.SetActive(true);
        PauseButton.SetActive(false);
        //Time.timeScale = 0;
    }

    // Check if player landed.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            canJump = true;
    }
}