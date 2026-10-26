using UnityEngine;

public class CarAudioController : MonoBehaviour
{
    private AudioSource engineAudio;
    private PlayerCarController carController;

    public float minPitch = 0.8f;
    public float maxPitch = 2.0f;

    void Start()
    {
        engineAudio = GetComponent<AudioSource>();
        carController = GetComponent<PlayerCarController>();

        if (engineAudio != null)
        {
            engineAudio.loop = true;
            engineAudio.Stop(); // Ensure it's silent on start
        }
    }

    void Update()
    {
        if (engineAudio == null || carController == null) return;

        // Check if gas is being held or the car is moving
        bool isMoving = Mathf.Abs(carController.GetCurrentSpeed()) > 0.1f;

        if (isMoving)
        {
            if (!engineAudio.isPlaying)
            {
                engineAudio.Play();
            }

            // Gradually pitch up the sound as the car speeds up
            float speedPercent = Mathf.Abs(carController.GetCurrentSpeed()) / carController.maxForwardSpeed;
            engineAudio.pitch = Mathf.Lerp(minPitch, maxPitch, speedPercent);
        }
        else
        {
            // Stop sound when stopped
            if (engineAudio.isPlaying)
            {
                engineAudio.Stop();
            }
        }
    }


}