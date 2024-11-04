using UnityEngine;

public class BecomeParentless : MonoBehaviour
{
    private void Start()
    {
        transform.SetParent(null);
    }
}