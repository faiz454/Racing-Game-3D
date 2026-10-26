using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [Header("Speed Settings")]
    public float maxForwardSpeed = 15f;
    public float maxReverseSpeed = 6f;
    public float acceleration = 4f;
    public float naturalDrag = 3.5f;

    [Header("Steering Settings")]
    public float turnSpeed = 120f;
    public float steeringSmoothness = 6f;

    private Rigidbody rb;
    private float currentSpeed = 0f;
    private float currentTurnInput = 0f;
    private float targetTurnInput = 0f;

    private bool isAccelerating = false;
    private bool isReversing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }

    void FixedUpdate()
    {
        // 1. Gradual Acceleration & Reverse Logic
        if (isAccelerating)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxForwardSpeed, acceleration * Time.fixedDeltaTime);
        }
        else if (isReversing)
        {
            // Gives an extra push when reversing to help un-stick from walls
            currentSpeed = Mathf.MoveTowards(currentSpeed, -maxReverseSpeed, acceleration * 1.5f * Time.fixedDeltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, naturalDrag * Time.fixedDeltaTime);
        }

        // 2. Apply velocity while maintaining gravity
        Vector3 moveDirection = transform.forward * currentSpeed;
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);

        // 3. Smooth Steering Interpolation
        currentTurnInput = Mathf.MoveTowards(currentTurnInput, targetTurnInput, steeringSmoothness * Time.fixedDeltaTime);

        if (Mathf.Abs(currentTurnInput) > 0.01f && Mathf.Abs(currentSpeed) > 0.1f)
        {
            float directionalTurn = currentTurnInput * turnSpeed * Mathf.Sign(currentSpeed) * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, directionalTurn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }

    // --- Mobile Button Methods ---
    public void GasPressed() { isAccelerating = true; isReversing = false; }
    public void GasReleased() { isAccelerating = false; }
    public void ReversePressed() { isReversing = true; isAccelerating = false; }
    public void ReverseReleased() { isReversing = false; }

    public void TurnLeftPressed() { targetTurnInput = -1f; }
    public void TurnRightPressed() { targetTurnInput = 1f; }
    public void StopTurn() { targetTurnInput = 0f; }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}