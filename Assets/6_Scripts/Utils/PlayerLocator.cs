using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    public static PlayerLocator instance;
    public Transform playerTransform { get; private set; }
    public Vector3 DefaultPosition;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        DefaultPosition = playerTransform.position;
    }
}