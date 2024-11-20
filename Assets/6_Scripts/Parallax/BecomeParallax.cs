using UnityEngine;

public class BecomeParallax : MonoBehaviour
{
    void Start()
    {
        transform.position = transform.parent.position;
        transform.SetParent(null);
    }

}
