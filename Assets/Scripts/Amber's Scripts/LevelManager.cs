using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int totalObjectives;
    [SerializeField] private int completedObjectives = 0;

    // checks if all necessary objectives are done
    public bool AreObjectivesComplete()
    {
        return totalObjectives == completedObjectives;
    }

    // completed a new objective
    public void CompleteObjective()
    {
        // guard against overincrementing
        if (completedObjectives < totalObjectives)
        {
            completedObjectives++;
        }
    }

}


