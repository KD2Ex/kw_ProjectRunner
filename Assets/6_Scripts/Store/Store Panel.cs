using UnityEngine;

public class StorePanel : MonoBehaviour
{
    [SerializeField] private InputReader input;

    private void OnEnable()
    {
        input.DisableGameplayInput();
    }

    private void OnDisable()
    {
        input.EnableGameplayInput();
    }
}
