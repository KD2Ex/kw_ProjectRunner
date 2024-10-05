using UnityEngine;

[CreateAssetMenu(fileName = "Float", menuName = "Scriptable Objects/Data/Float var")]
public class FloatVariable : ScriptableObject
{
    public float Value;
    public bool ResetOnEnable;

    private void OnEnable()
    {
        if (ResetOnEnable) Value = 0f;
    }
}
