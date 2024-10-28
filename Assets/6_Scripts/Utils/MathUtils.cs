using UnityEngine;

public class MathUtils
{
    public static bool IsFloatInBounds(float value, float upperBound, float lowerBound)
    {
        return value >= lowerBound && value <= upperBound;
    }

    public static Color GetColorWithAlpha(Color color, float a)
    {
        return new Color(color.r, color.g, color.b, a);
    }
}