using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] protected FloatVariable m_Duration;
    protected float elapsedTime = 0f;
    
    private void OnEnable()
    {
        // play sound
        // play animation
    }

    private void OnDisable()
    {
        elapsedTime = 0f;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > m_Duration.Value)
        {
            gameObject.SetActive(false);
        }
    }
    
    public void AddDuration()
    {
        elapsedTime -= m_Duration.Value;
    }
}