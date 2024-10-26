using UnityEngine;

public class CarCrash : Enemy
{
    [SerializeField] private InputReader input;
    [SerializeField] private CarCrashAnimation anim;
    
    private void OnEnable()
    {
        input.InteractEvent += Skip;
        GameManager.instance.IsEventChunkRunning = true;
    }

    private void OnDisable()
    {
        input.InteractEvent -= Skip;
        GameManager.instance.IsEventChunkRunning = false;
    }

    private void Skip()
    {
        anim.EventEnded();
    }

    // Update is called once per frame
    void Update()
    {
        if (DistanceToPlayer < 5f)
        { 
            transform.SetParent(null);
            
            anim.transform.SetParent(null);
            anim.transform.position = new Vector3(-8.3f, -1.6f, 0f);
            anim.gameObject.SetActive(true);
        }       
    }
}
