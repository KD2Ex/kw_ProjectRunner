using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CarnavalBalloons : MonoBehaviour
{
    [SerializeField] private GameObject[] balloons;
    [SerializeField] private GameObject bee;
    private bool replaced;

    private void OnEnable()
    {
        GameManager.instance.CarnavalCrashedBalloons = 0;
    }

    private void Start()
    {
        Debug.Log(GameManager.instance.CarnavalCrashedBalloons);
    }

    private void Update()
    {
        if (replaced) enabled = false;
        
        if (GameManager.instance.CarnavalCrashedBalloons > 5)
        {
            Replace();
            replaced = true;
        }
    }

    private void Replace()
    {
        foreach (var balloon in balloons)
        {
            if (!balloon) return;
            if (balloon.transform.position.x  - GameManager.instance.Player.transform.position.x < 20f) continue;
            
            var inst = Instantiate(bee, balloon.transform.position, Quaternion.identity, transform);
            var pos = inst.transform.position;
            inst.transform.position = new Vector3(pos.x, pos.y + 1.25f, 0f);
            inst.transform.localScale = balloon.transform.localScale / 3;
            
            Destroy(balloon);
        }
    }
}