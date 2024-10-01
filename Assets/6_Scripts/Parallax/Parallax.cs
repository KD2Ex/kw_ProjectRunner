using UnityEngine;

public class Parallax : MonoBehaviour
{
    //[SerializeField] private InputReader inputReader;
    [SerializeField] private FloatVariable so_Speed;
    [SerializeField] private GameObject background;
    [SerializeField] private bool instantiateFirstBackground;
    
    [Range(0, 1f)]
    [SerializeField] private float parallaxEffectMultiplier;  // Controls the speed of the parallax effect

    private Transform player;
    
    private SpriteRenderer sprite;
    private GameObject nextBackground;
    
    private Vector3 lastCameraPosition;  // Stores the last position of the camera

    private float screenSize = 18f;

    private bool isBackgroundLoaded;
    private bool isBackgroundCleared;
    public Bounds Bounds { get; private set; }

    private Transform cameraTransform => Camera.main.transform;

    /*
    private void OnEnable()
    {
        inputReader.RunEvent += OnMove;
    }

    private void OnDisable()
    {
        inputReader.RunEvent -= OnMove;
    }

    private void OnMove(bool value) => m_Running = value;*/
    
    private void MoveIllusion()
    {
        transform.Translate(Vector2.left * (so_Speed.Value * parallaxEffectMultiplier * Time.deltaTime));
    }
    
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (instantiateFirstBackground)
        {
            background = Instantiate(background, transform);
        }
        
        sprite = background.GetComponent<SpriteRenderer>();
        SetNewBounds(sprite.bounds);
        
        // Set the last camera position to the camera's starting position
        lastCameraPosition = cameraTransform.position;

        Debug.Log(Bounds);
    }

    private void Locomotion()
    {
        MoveIllusion();
    }

    void Update()
    {
        Locomotion();
        
        var distanceToPlayer = Bounds.extents.x + background.transform.position.x - player.position.x;

        AddBackground(distanceToPlayer);
        RemoveBackground(distanceToPlayer);
    }

    private void AddBackground(float distanceToPlayer)
    {
        if (distanceToPlayer < screenSize + 2f && !isBackgroundLoaded)
        {
            Debug.Log("Load");    
            
            DuplicateBackground(background);
            isBackgroundLoaded = true;
            isBackgroundCleared = false;
        }
    }

    private void RemoveBackground(float distanceToPlayer)
    {
        if (distanceToPlayer < -6f && !isBackgroundCleared)
        {
            Debug.Log("Clear");            
            /*prevBackground.SetActive(false);*/

            ClearBackground(background);
            
            isBackgroundCleared = true;
            isBackgroundLoaded = false;
        }
    }
    
    private void Move()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        deltaMovement.z = 0f;
        Debug.Log(deltaMovement);
        
        transform.Translate(deltaMovement * (parallaxEffectMultiplier * 50 * Time.deltaTime));
        //transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier * 50 * Time.deltaTime, 0);
        lastCameraPosition = cameraTransform.position;
    }

    private void DuplicateBackground(GameObject back)
    {
        var next = Instantiate(back, transform);
        next.transform.position += new Vector3(Bounds.extents.x * 2, 0, 0);
        nextBackground = next;
    }

    private void ClearBackground(GameObject back)
    {
        back.SetActive(false);
        background = nextBackground;
        nextBackground = null;
    }
    
    private void SetNewBounds(Bounds bounds)
    {
        Bounds = bounds;
    }
}
