using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private bool instantiateFirstBackground;
    public float parallaxEffectMultiplier;  // Controls the speed of the parallax effect
    
    private Transform player;
    
    private SpriteRenderer sprite;
    private SpriteRenderer nextSprite;
    private GameObject nextBackground;
    
    private Vector3 lastCameraPosition;  // Stores the last position of the camera

    private float screenSize = 18f;

    private bool isBackgroundLoaded;
    private bool isBackgroundCleared;
    public Bounds Bounds { get; private set; }

    private Transform cameraTransform => Camera.main.transform;
    
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

    void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, 0);
        lastCameraPosition = cameraTransform.position;

        var distanceToPlayer = Bounds.extents.x + background.transform.position.x - player.position.x;

        if (distanceToPlayer < screenSize + 2f && !isBackgroundLoaded)
        {
            Debug.Log("Load");    
            
            DuplicateBackground(background);
            SetNewBounds(nextSprite.bounds);

            isBackgroundLoaded = true;
            isBackgroundCleared = false;
        }


        if (distanceToPlayer < -3f && !isBackgroundCleared)
        {
            Debug.Log("Clear");            
            /*prevBackground.SetActive(false);*/

            ClearBackground(background);
            
            isBackgroundCleared = true;
            isBackgroundLoaded = false;
        }
    }

    private void DuplicateBackground(GameObject back)
    {
        var next = Instantiate(back, transform);
        next.transform.position += new Vector3(Bounds.extents.x * 2, 0, 0);
        nextBackground = next;
        nextSprite = next.GetComponent<SpriteRenderer>();
    }

    private void ClearBackground(GameObject back)
    {
        back.SetActive(false);
        background = nextBackground;
        nextBackground = null;

        sprite = nextSprite;
        nextSprite = null;
    }
    
    private void SetNewBounds(Bounds bounds)
    {
        Bounds = bounds;
    }
}
