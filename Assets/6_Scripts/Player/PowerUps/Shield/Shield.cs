using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Shield : MonoBehaviour
{
    [SerializeField] private FloatVariable m_Duration;
    private float elapsedTime = 0f;
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnEnable()
    {
        // play sound
        // play animation
    }

    private void OnDisable()
    {
        elapsedTime = 0f;
        player.GetShielded(false);
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
