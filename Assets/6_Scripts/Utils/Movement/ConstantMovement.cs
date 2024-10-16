using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    [SerializeField] private FloatVariable so_Speed;
    
    [Range(0, 2)]
    [SerializeField] private float m_Multiplier;
    [SerializeField] private bool m_AutoMoveLeft;
    private float m_Speed => so_Speed.Value;
    
    private void Update()
    {
        if (!m_AutoMoveLeft) return;
        
        Move(Vector2.left);
    }

    public void Move(Vector2 direction)
    {
        transform.Translate(direction * (m_Speed * m_Multiplier * Time.deltaTime));
    }
}
