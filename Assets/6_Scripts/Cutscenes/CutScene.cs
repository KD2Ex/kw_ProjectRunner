using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutScene : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject[] vods;

    private int currentVod = 0;
    private List<CutsceneNode> nodes = new();
    
    public UnityEvent OnEnd;

    private bool canPlay;
    
    private void Awake()
    {
        for (int i = 0; i < vods.Length; i++)
        {
            CutsceneNode node = null;
            if (i == 0)
            {
                node = new CutsceneNode(vods[i], null);
            }
            else if (i == vods.Length - 1)
            {
                node = new CutsceneNode(vods[i], vods[i - 1]);
            }

            node ??= new CutsceneNode(vods[i], vods[i - 1]);
            
            nodes.Add(node);
        }
    }

    private void OnEnable()
    {
        input.InteractEvent += Play;
    }

    private void OnDisable()
    {
        input.InteractEvent -= Play;
    }

    private void Update()
    {
        canPlay = GameManager.instance.MovementSpeed.Value < .0001f;
    }

    public void Play()
    {
        if (!canPlay) return;
        
        Debug.Log("Play");
        
        if (currentVod == 0)
        {
            EnterCutscene();
        }
        
        if (currentVod == vods.Length)
        {
            ExitCutscene();
            nodes[^1].vod.SetActive(false);
            return;
        }
        
        var currNode = nodes[currentVod];

        if (currNode.prev)
        {
            currNode.prev.SetActive(false);
        }
        
        currNode.vod.SetActive(true);
        currentVod++;
    }

    private void EnterCutscene()
    {
        Time.timeScale = 0f;
    }

    private void ExitCutscene()
    {
        currentVod = 0;
        Time.timeScale = 1f;
    }
}

public class CutsceneNode
{
    public CutsceneNode(GameObject vod, GameObject prev)
    {
        this.vod = vod;
        this.prev = prev;
    }

    public GameObject vod { get; }
    public GameObject prev { get; }
}