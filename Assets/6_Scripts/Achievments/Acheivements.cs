using System;
using System.Collections.Generic;
using UnityEngine.UI;

public struct AchievmentsConditions
{
    public string name;
    public Func<bool> condition;
}

public class Achievements
{
    public List<Achievement> achievements = new ();


    private void CompleteLocationOne()
    {
        achievements[0].Completed = true;
    }
}

[Serializable]
public class Achievement
{
    public string Title;
    public string Description;
    public Image Icon;
    public Func<bool> Condition;
    public bool Completed;
}   