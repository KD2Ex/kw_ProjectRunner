using System.Collections;
using UnityEngine;

public class StaringMan : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private InputReader input;
    [SerializeField] private Transform staringMan;

    [SerializeField] private Transform[] points;

    private WaitForFixedUpdate waiter = new ();
    
    private float distanceToPlayer => GameManager.instance.Player.transform.position.x - transform.position.x;

    private bool triggered = false;
    private Vector3 startingPos;
    private Vector3[] pointPositions;
    
    private void OnEnable()
    {
        pointPositions = new Vector3 [points.Length];
        startingPos = staringMan.localPosition;
        for (int i = 0; i < points.Length; i++)
        {
            pointPositions[i] = points[i].localPosition;
        }
        GameManager.instance.IsEventChunkRunning = true;
    }

    private void OnDisable()
    {
        input.EnableGameplayInput();
        GameManager.instance.IsEventChunkRunning = false;
    }

    private void Update()
    {
        if (triggered) return;

        if (distanceToPlayer < 5f)
        {
            triggered = true;
            Execute();
        }
    }

    private void Execute()
    {
        source.Play();
        GameManager.instance.SceneMusic.Source.Pause();

        
        for (int i = 0; i < points.Length; i++)
        {
            points[i].SetParent(null);
            points[i].position = pointPositions[i];
        }

        staringMan.SetParent(null);
        staringMan.position = startingPos;
        StartCoroutine(Execution());
    }

    private IEnumerator Execution()
    {
        var distance = 0f;
        do
        {
            distance = (points[0].position - staringMan.position).magnitude;
            staringMan.Translate(Vector2.up * (3f * Time.deltaTime));
            yield return waiter;
        } while (distance > .2f);

        yield return new WaitForSeconds(2f);
        
        do
        {
            distance = (points[1].position - staringMan.position).magnitude;
            staringMan.Translate(Vector2.up * (5f * Time.deltaTime));
            yield return waiter;
        } while (distance > .2f);

        yield return new WaitForSeconds(source.clip.length - source.time - .5f);

        var finalPoint = new Vector3(startingPos.x, -12f, 0f);
        
        do
        {
            distance = (finalPoint - staringMan.position).magnitude;
            staringMan.Translate(Vector2.down * (12f * Time.deltaTime));
            yield return waiter;
        } while (distance > .1f);
        
        GameManager.instance.SceneMusic.Source.UnPause();

        for (int i = 0; i < points.Length; i++)
        {
            Destroy(points[i].gameObject);
        }
        gameObject.SetActive(false);
        Destroy(staringMan.gameObject);
    }
}