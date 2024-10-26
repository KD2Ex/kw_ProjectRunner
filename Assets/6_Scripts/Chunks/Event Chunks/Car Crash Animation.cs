using UnityEngine;

public class CarCrashAnimation : MonoBehaviour
{
    [SerializeField] private CarCrash carCrash;

    public void EventEnded()
    {
        gameObject.SetActive(false);
        carCrash.gameObject.SetActive(false);
    }
}
