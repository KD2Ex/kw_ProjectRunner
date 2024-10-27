using UnityEngine;

public class EightShapeMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private bool reverse;
    
    private Vector3 offset;
    
    private void Start()
    {
        offset = transform.position;
    }

    private float t;
    private float xPos;
    private float yPos;
    
    private void Update()
    {
        var multiplier = reverse ? -1f : 1f;

        t += speed * Time.deltaTime;
        xPos = Mathf.Cos(t) * multiplier;                        
        yPos = Mathf.Sin(2 * t) / 2;

        transform.position = new Vector3(xPos, yPos, 0f) + offset;
    }
}