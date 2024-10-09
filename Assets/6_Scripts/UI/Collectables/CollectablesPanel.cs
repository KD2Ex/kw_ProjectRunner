using UnityEngine;

public class CollectablesPanel : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject FoodLine;
    
    private void OnEnable()
    {
        FoodLine.SetActive(true);
        input.DisableGameplayInput();
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        FoodLine.SetActive(false);
        input.EnableGameplayInput();
        Time.timeScale = 1f;
    }
}
