using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    public RaceManager raceManager;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the Player car crossed
        if (other.GetComponent<PlayerCarController>() != null)
        {
            raceManager.EndRace("VICTORY! 1st Rank");
        }
        // Check if the AI car crossed
        else if (other.GetComponent<AINavWaypointRacer>() != null)
        {
            raceManager.EndRace("DEFEAT! 2nd Rank");
        }
    }
}