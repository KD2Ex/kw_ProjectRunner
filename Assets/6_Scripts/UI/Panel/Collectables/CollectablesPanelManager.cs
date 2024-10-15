using UnityEngine;

public class CollectablesPanelManager : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject panel;
    
    private void OnEnable()
    {
        input.EscEvent += Toggle;
    }

    private void OnDisable()
    {
        input.EscEvent -= Toggle;
    }

    private void Toggle()
    {
        panel.SetActive(!panel.gameObject.activeInHierarchy);
    }

}
