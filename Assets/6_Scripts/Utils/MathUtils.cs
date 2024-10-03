public class MathUtils
{
    public static bool IsFloatInBounds(float value, float upperBound, float lowerBound)
    {
        return value >= lowerBound && value <= upperBound;
    }       
}