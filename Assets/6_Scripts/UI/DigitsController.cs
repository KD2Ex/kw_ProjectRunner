using UnityEngine;

public class DigitsController : MonoBehaviour
{
    [SerializeField] private FloatVariable Data;
    [SerializeField] private float xGap;
    [SerializeField] private DigitObject digitObject;
    [SerializeField] private Transform spawnPoint;
    private float Value => Data.Value;
    
    public void UpdateUI()
    {
        int intValue = (int)Value;
        int index = 0;
        
        while (intValue > 0)
        {
            int digit = intValue % 10;
            intValue /= 10;
            
            CreateDigit(digit, index);
            index++;
        }
    }

    private void CreateDigit(int value, int index)
    {
        var digit = Instantiate<DigitObject>(digitObject, spawnPoint);

        var x = index * xGap;
        
        digit.transform.position = new Vector3(spawnPoint.position.x + index * xGap * -1, transform.position.y, 0f);
        digit.Initialize(value);
        
        Debug.Log($"Digit: {value}");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
