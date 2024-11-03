using System.Collections.Generic;

public class Cats : BossAppearance
{
    public List<CatAppear> heads = new();
    
    public override void Appear()
    {
        base.Appear();
        
        foreach (var head in heads)
        {
            head.gameObject.SetActive(true);
        }
        transform.SetParent(null);
    }

    public override void Disappear()
    {
        base.Disappear();
        foreach (var head in heads)
        {
            head.Disappear();
        }
    }
}