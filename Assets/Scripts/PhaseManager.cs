using UnityEngine;

public class PhaseManager : MonoBehaviour 
{
    Phase currentPhase;
    int floor = 1;

    public void SetPhase(Phase phaseToSet)
    {
        currentPhase = phaseToSet;
    }

    public Phase GetCurrentPhase()
    {
        return currentPhase;
    }

    public void SetFloor(int floorToSet)
    {
        floor = floorToSet;
    }

    public int GetCurrentFloor()
    {
        return floor;
    }
}