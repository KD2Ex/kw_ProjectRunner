using UnityEngine;
using UnityEngine.InputSystem;

public class BossTutorial : MonoBehaviour
{
    [SerializeField] private InputReader input;

    [SerializeField] private GameObject pc;
    [SerializeField] private GameObject gamepad;

    private bool inputDevice;

    private void Awake()
    {
        inputDevice = false;// Gamepad.current.enabled;

        input.DisableBossGameplayInput();
        
        
        if (inputDevice)
        {
            gamepad.SetActive(true);
            pc.SetActive(false);
        }
        else
        {
            gamepad.SetActive(false);
            pc.SetActive(true);
        }
    }

    private void Start()
    {
        Time.timeScale = 0f;
    }

    private void OnEnable()
    {
        input.InteractEvent += Skip;
    }

    private void OnDisable()
    {
        input.InteractEvent -= Skip;
    }

    private void Skip()
    {
        GameManager.instance.SceneMusic.Source.Play();
        
        input.EnableBossGameplayInput();
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}