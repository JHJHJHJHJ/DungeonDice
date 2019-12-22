using UnityEngine;

public class StateHolder : MonoBehaviour
{
    [SerializeField] Phase currentPhase;
    int floor = 1;

    public delegate void SetAndHandlePhaseDelegate();
    public SetAndHandlePhaseDelegate SetPhaseToExplore;
    public SetAndHandlePhaseDelegate SetPhaseToEvent;
    public SetAndHandlePhaseDelegate SetPhaseToCombat;

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

