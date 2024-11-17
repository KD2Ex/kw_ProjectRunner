using UnityEngine;

public class EventChunk : Enemy
{
    [SerializeField] private InputReader input;
    [SerializeField] private EventAnimation anim;

    private Vector3 animPosition;

    private bool triggered;
    
    private void OnEnable()
    {
        animPosition = anim.transform.localPosition;
        //input.InteractEvent += Skip;
        GameManager.instance.IsEventChunkRunning = true;
    }

    private void OnDisable()
    {
        input.EnableGameplayInput();
        //input.InteractEvent -= Skip;
        GameManager.instance.IsEventChunkRunning = false;
    }

    private void Skip()
    {
        anim.EventEnded();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered) return;
        
        if (DistanceToPlayer < 5f)
        {
            TriggerEvent();
        }       
    }

    private void TriggerEvent()
    {
        triggered = true;
            
        transform.SetParent(null);
        anim.transform.SetParent(null);
        anim.transform.position = animPosition;//new Vector3(-8.3f, -1.6f, 0f);
        anim.gameObject.SetActive(true);
        input.DisableGameplayInput();
        
        GameManager.instance.SceneMusic.Source.Pause();
    }
}
