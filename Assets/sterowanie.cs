using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    public float maxMoveSpeed = 10f;
    public float moveSpeedMultiplier = 10f;
    public float moveSpeed = 8f;
    public float jumpPower = 3f;
    public float maxJumpTime = 1f;
    public float deadZone = 0.05f;
    public float deathHeight = -10f;
    [SerializeField] private GameOverUI gameOverUI;
    private Rigidbody rb;
    private Vector3 calibrationOffset = Vector3.zero;
    private float jumpTimeCounter;

    private bool isJumping = false;
    private bool isGrounded = true;
    private bool onIce = false;

    private Vector3 currentVelocity = Vector3.zero;
    public float iceFriction = 0.9999f;
    public float normalFriction = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Calibrate();
        LoadSpawnChances();
    }

    void FixedUpdate()
    {
        if (transform.position.y < deathHeight)
        {
            Die();
            return;
        }

        Vector3 tilt = Input.acceleration - calibrationOffset;
        float xTilt = tilt.x;

        if (Mathf.Abs(xTilt) < deadZone)
            xTilt = 0;

        float moveSpeed = xTilt * moveSpeedMultiplier;
        moveSpeed = Mathf.Clamp(moveSpeed, -maxMoveSpeed, maxMoveSpeed);

        Vector3 move = new Vector3(moveSpeed, 0, 0);

        // Tarcie (ślizganie)
        if (onIce)
            currentVelocity = Vector3.Lerp(currentVelocity, move, 1 - iceFriction);
        else
            currentVelocity = Vector3.Lerp(currentVelocity, move, 1 - normalFriction);

        rb.MovePosition(rb.position + currentVelocity * Time.fixedDeltaTime);

        // SKOK
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Rozpoczęcie skoku
            if (touch.phase == UnityEngine.TouchPhase.Began && isGrounded)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, 0f); // Reset pionowej prędkości
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // Mocne wybicie
                isJumping = true;
                isGrounded = false;
                jumpTimeCounter = 0f;
            }

            // Trzymanie dotyku – kontynuacja skoku
            if (touch.phase == UnityEngine.TouchPhase.Stationary && isJumping)
            {
                if (jumpTimeCounter < maxJumpTime)
                {
                    rb.AddForce(Vector3.up * jumpPower * 0.5f, ForceMode.Acceleration); // delikatna kontynuacja
                    jumpTimeCounter += Time.fixedDeltaTime;
                }
            }

            // Puszczenie palca – zakończenie skoku
            if (touch.phase == UnityEngine.TouchPhase.Ended || touch.phase == UnityEngine.TouchPhase.Canceled)
            {
                isJumping = false;
            }
        }
    }

    public void Calibrate()
    {
        calibrationOffset = Input.acceleration;
        Debug.Log("Kalibracja wykonana: " + calibrationOffset);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("static") || collision.gameObject.CompareTag("Ice"))
        {
            isGrounded = true;
            isJumping = false;
        }

        if (collision.gameObject.CompareTag("Ice"))
            onIce = true;

        if (collision.gameObject.CompareTag("Spikes") || collision.gameObject.CompareTag("Enemy"))
            Die();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
            onIce = false;
    }

    void Die()
    {
        Debug.Log("Gracz zginął!");
        
        gameOverUI.ShowGameOver();
        gameObject.SetActive(false); // Ukryj gracza
    }
    public void LoadSpawnChances()
    {
        jumpPower = PlayerPrefs.GetFloat("jumpPower", jumpPower);
        moveSpeed = PlayerPrefs.GetFloat("moveSpeed", moveSpeed);
    }

}
