using UnityEngine;

public class PhaseManager : MonoBehaviour 
{
    Phase currentPhase;

    public void SetPhase(Phase phaseToSet)
    {
        currentPhase = phaseToSet;
    }

    public Phase GetCurrentPhase()
    {
        return currentPhase;
    }
}