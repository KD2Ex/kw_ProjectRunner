using System.Collections.Generic;
using UnityEngine;

public class UIDigitsTimer : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private DigitObject digit;
    private List<DigitObject> digits = new();
    
    [SerializeField] private FloatVariable minutes;
    [SerializeField] private FloatVariable seconds;

    private void Awake()
    {
        //GameManager.instance.UITimer = this;
        
        foreach (var point in spawnPoints)
        {
            var instance = Instantiate(digit, point.transform);
            digits.Add(instance);
        }
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        digits[0].SetDigit((int)(minutes.Value / 10));
        digits[1].SetDigit((int)(minutes.Value % 10));
        digits[2].SetDigit((int)(seconds.Value / 10));
        digits[3].SetDigit((int)(seconds.Value % 10));
    }
    
    public void Load(IntSaveData data)
    {
        minutes.Value = data.Value / 60;
        seconds.Value = data.Value % 60;
    }
}