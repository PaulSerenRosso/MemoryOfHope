using UnityEngine;

static class ConditionState
{
    public static bool Timer(float duration, float timer)
    {
        if (duration < timer)
        {
            return true; // Fin du timer
        }
        return false; // Timer non terminé
    }

    public static bool CheckDistance(Vector3 begin, Vector3 end, float minDistance)
    {
        if (Vector3.Distance(begin, end) < minDistance)
        {
            return true; // Distance inférieure à minDistance
        }
        return false; // Distance supérieure à minDistance
    }
}
