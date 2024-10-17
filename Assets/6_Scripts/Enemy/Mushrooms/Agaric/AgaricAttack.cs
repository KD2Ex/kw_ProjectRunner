using UnityEngine;

public class AgaricAttack : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private void Update()
    {
        if (transform.position.x < PlayerLocator.instance.playerTransform.position.x - 20f)
        {
            Destroy(gameObject);
        }
        
        transform.Translate(Vector3.left * (speed * Time.deltaTime));
    }
}