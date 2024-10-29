using System;
using System.Collections;
using UnityEngine;

public class Fox : MonoBehaviour
{
    [SerializeField] private FloatVariable speed;

    private FoodUseManager foodUse;
    
    private Vector2 target;
    
    private Transform player;
    private Rigidbody2D rb;

    private float offset = 4f;
    
    private void Awake()
    {
        player = PlayerLocator.instance.playerTransform;
        rb = GetComponent<Rigidbody2D>();
        foodUse = FindObjectOfType<FoodUseManager>();
    }

    private void OnEnable()
    {
        StartCoroutine(Move(player, offset));
        transform.SetParent(null);
        offset -= 1f;

        Debug.Log("fox enabled");
        
        foodUse.BlockConsuming();
    }

    private void OnDisable()
    {
        foodUse.UnblockConsuming();
    }

    private void Update()
    {
        if (Mathf.Approximately(speed.Value, 0f))
        {
            StopAllCoroutines();
            StartCoroutine(Move());
        }

        if (transform.position.x > player.position.x && transform.position.x - player.position.x > 25f)
        {
            Destroy(gameObject);
        }
    }

    public void GetCloser()
    {
        StartCoroutine(Move(player, offset));
        offset -= 1f;
    }
    
    private IEnumerator Move()
    {
        rb.MovePosition(transform.position + Vector3.right * (30f * Time.deltaTime));
        yield return null;
    }
    
    private IEnumerator Move(Transform to, float offset)
    {
        var dir = (to.position - transform.position) - new Vector3(offset, 0f, 0f);
        while (dir.x > .03f)
        {
            Debug.Log($"{dir.x}");
            
            rb.MovePosition(transform.position + Vector3.Normalize(dir) * (30f * Time.deltaTime));
            dir = (to.position - transform.position) - new Vector3(offset, 0f, 0f);
            yield return new WaitForFixedUpdate();
        }
    }
}