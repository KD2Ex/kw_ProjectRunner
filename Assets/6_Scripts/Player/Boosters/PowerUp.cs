using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] protected FloatVariable m_Duration;
    protected float elapsedTime = 0f;
    
    protected virtual void OnEnable()
    {
        // play sound
        // play animation
    }

    protected virtual void OnDisable()
    {
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > m_Duration.Value)
        {
            gameObject.SetActive(false);
            elapsedTime = 0f;

        }
    }
    
    public void AddDuration()
    {
        elapsedTime -= m_Duration.Value;
    }
}