using System;

public class FuncPredicate : IPredicate
{
    private Func<bool> predicate;

    public FuncPredicate(Func<bool> predicate)
    {
        this.predicate = predicate;
    }

    public bool Evaluate() => predicate.Invoke();
}