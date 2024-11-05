using UnityEngine;

public static class Utils
{
    public static float DistanceToPlayer(Transform transform)
    {
        return PlayerLocator.instance.DistanceToPlayer(transform);
    }
}