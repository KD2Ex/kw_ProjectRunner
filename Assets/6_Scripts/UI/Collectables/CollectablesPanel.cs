using UnityEngine;

public class CollectablesPanel : MonoBehaviour
{
    [SerializeField] private InputReader input;

    private void OnEnable()
    {
        input.DisableGameplayInput();
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        input.EnableGameplayInput();
        Time.timeScale = 1f;
    }
}
