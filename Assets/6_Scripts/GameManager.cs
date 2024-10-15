using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player Player { get; private set; }
    public Coins Coins { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Player = FindObjectOfType<Player>();
        Coins = FindObjectOfType<Coins>();
    }
}