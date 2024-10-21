using System.Collections.Generic;
using UnityEngine;

public class DigitsController : MonoBehaviour
{
    [SerializeField] private FloatVariable Data;
    [SerializeField] private float xGap;
    [SerializeField] private DigitObject digitObject;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private int maxAmount;
    
    [SerializeField] private bool UseExternalData;
    private float data;
    
    private List<DigitObject> digits = new();
    
    private float Value => UseExternalData ? data : Data.Value;
    
    public void UpdateUI()
    {
        int intValue = (int)Value;
        int index = 0;

        if (intValue == 0)
        {
            foreach (var digit in digits)
            {
                digit.SetDigit(0);
            }
        }
        
        while (intValue > 0)
        {
            int digit = intValue % 10;
            intValue /= 10;

            if (digits.Count == index)
            {
                var instance = CreateDigit(digit, index);
                digits.Add(instance);
                Align();
            }
            else
            {
                digits[index].SetDigit(digit);
            }
            index++;
        }

        
        if (digits.Count > index && maxAmount == 0)
        {
            for (int i = digits.Count - 1; i > index - 1; i--)
            {
                Debug.Log(digits[i].transform.position.x);
                Destroy(digits[i].gameObject);
                digits.Remove(digits[i]);
            }
        }
        
        for (int i = digits.Count; i < maxAmount; i++)
        {
            digits.Add(CreateDigit(0, i));
            Align();
        }

        
    }

    public DigitObject CreateDigit(int value, int index)
    {
        var digit = Instantiate<DigitObject>(digitObject, spawnPoint);

        var pos = new Vector3(spawnPoint.position.x + index * xGap * -1, transform.position.y, 0f);
        var localPos = new Vector3(index * xGap * -1, 0f, 0f);
        //digit.transform.position = pos;
        
        digit.transform.localPosition = localPos;
        digit.SetDigit(value);
        
        return digit;
    }

    public void Align()
    {
        spawnPoint.transform.SetParent(null);
        var length = digits.Count * .35f + (digits.Count - 1) * xGap;
        var center = spawnPoint.position.x - length / 4;

        transform.position = new Vector3(center, transform.position.y, 0f);
        spawnPoint.transform.SetParent(transform);
    }

    public void SetData(float value)
    {
        data = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }
}
