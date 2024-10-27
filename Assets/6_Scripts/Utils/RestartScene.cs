using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private string sceneName;

    private void OnEnable()
    {
        input.RestartScenesEvent += Restart;
    }

    private void OnDisable()
    {
        input.RestartScenesEvent -= Restart;
    }

    private void Restart()
    {
        SceneManager.LoadScene(sceneName);
    }
}
