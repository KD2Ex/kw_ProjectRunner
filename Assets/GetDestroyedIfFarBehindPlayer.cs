using UnityEngine;

public class GetDestroyedIfFarBehindPlayer : MonoBehaviour
{
    [SerializeField] private FloatVariable m_DistanceToDestroy;
    
    private Vector3 PlayerPosition => PlayerLocator.instance.playerTransform.position;

    void Update()
    {
        var distance = (PlayerPosition - transform.position).magnitude;
        
        if (distance > m_DistanceToDestroy.Value && PlayerPosition.x > transform.position.x) gameObject.SetActive(false);
    }
}
