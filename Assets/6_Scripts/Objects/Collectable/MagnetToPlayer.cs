using System;
using System.Transactions;
using UnityEngine;

public class MagnetToPlayer : MonoBehaviour
{
    [SerializeField] private float force;
    private Transform player;
    private Rigidbody2D rb;

    private bool started;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!started) return;
        var dir = (player.position - transform.position).normalized;
        transform.Translate(dir * (force * Time.deltaTime));
    }

    private void FixedUpdate()
    {
        return;
        if (!started) return;
        var dir = (player.position - transform.position).normalized;
        rb.MovePosition(transform.position + dir * (force * Time.deltaTime));
    }

    public void Execute()
    {
        started = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (!other.CompareTag("Magnet")) return;
        player = other.transform;
        Execute();
    }
}
