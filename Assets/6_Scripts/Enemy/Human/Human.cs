using System.Collections;
using UnityEngine;

public class Human : Enemy
{
    [SerializeField] private float triggerAppearMusicDist;
    [SerializeField] private float speed;

    [SerializeField] private AudioClip appearClip;
    
    private void Start()
    {
        //transform.SetParent(null);

        StartCoroutine(WaitForAppear());
        StartCoroutine(WaitForTriggerShake());
    }

    private IEnumerator WaitForAppear()
    {
        while (DistanceToPlayer > triggerAppearMusicDist)
        {
            yield return null;
        }
        
        SoundFXManager.instance.PlayClipAtPoint(appearClip, transform, 1f);
    }

    private IEnumerator WaitForTriggerShake()
    {
        while (DistanceToPlayer > 14f)
        {
            yield return null;
        }
        
        GameManager.instance.CameraShake.Execute();
    }

    private void Update()
    {
        if (PlayerLocator.instance.DistanceToPlayer(transform) < distance)
        {
            transform.Translate(Vector3.left * (speed * Time.deltaTime));
        }
        
    }
}
