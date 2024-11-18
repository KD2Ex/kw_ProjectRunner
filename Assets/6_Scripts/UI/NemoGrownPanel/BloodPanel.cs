using UnityEngine;
using UnityEngine.UI;

public class BloodPanel : MonoBehaviour
{
    [SerializeField] private SOListData<Color> colors;
    [SerializeField] private Image image;
    
    private int levelIndex => GameManager.instance.NemoCurrentLevel;
    
    private void OnEnable()
    {
        image.color = colors.Items[levelIndex];
    }
}