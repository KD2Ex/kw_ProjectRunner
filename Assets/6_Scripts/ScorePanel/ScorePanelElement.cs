using System;
using UnityEngine;

public abstract class ScorePanelElement : MonoBehaviour
{
    [SerializeField] protected ScorePanel scorePanel;

    public abstract void Execute();
    public abstract void Stop();
}