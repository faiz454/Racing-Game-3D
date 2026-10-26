using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object passing through has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Lap Completed! Player crossed the finish line.");
            // You can add your lap counting logic or UI updates here!
        }
    }
}