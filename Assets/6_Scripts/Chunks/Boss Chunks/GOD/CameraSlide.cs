using System.Collections;
using UnityEngine;

public class CameraSlide : MonoBehaviour
{
    [SerializeField] private Vector2 target;

    private Transform mainCamera => Camera.main.transform;
    private Vector2 origin;

    private WaitForFixedUpdate waiter = new();
    
    private void Awake()
    {
        origin = mainCamera.transform.position;
    }

    public void Appear()
    {
        StartCoroutine(MoveTo(target, Vector2.left));
    }

    public void Disappear()
    {
        StartCoroutine(MoveTo(origin, Vector2.right));
    }

    private IEnumerator MoveTo(Vector2 to, Vector2 dir)
    {
        float dist;

        do
        {
            dist = (to - (Vector2) mainCamera.position).magnitude;

            mainCamera.Translate(dir * (5f * Time.deltaTime));
            yield return null;
        } while (dist > .1f);
    }
}