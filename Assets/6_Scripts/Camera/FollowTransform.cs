using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform m_Target;
    
    private void LateUpdate()
    {
        var pos = new Vector3(m_Target.position.x, m_Target.position.y, -10f);
        transform.position = pos;
    }
}
