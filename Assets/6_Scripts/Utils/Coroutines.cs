using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Coroutines
{
    public static IEnumerator WaitFor(float seconds, Action before = null, Action after = null)
    {
        before?.Invoke();
        yield return new WaitForSeconds(seconds);
        after?.Invoke();
    }
    
    private IEnumerator MoveTo(Transform to, Transform origin, float speed, float delay = 0f, Action after = null)
    {
        var waiter = new WaitForFixedUpdate();
        var Pos = origin.position;
        
        var dir = Vector3.Normalize((to.position - Pos).normalized);
        var dist = GetDistance();

        while (dist > .1f)
        {
            dist = GetDistance();
            origin.Translate(dir * (speed * Time.fixedDeltaTime));
            
            yield return waiter;
        }

        var elapsed = 0f;

        while (elapsed < delay)
        {
            elapsed += Time.fixedDeltaTime;
            yield return waiter;
        }

        after?.Invoke();
        yield break;

        float GetDistance()
        {
            return (to.position - Pos).magnitude;
        }
    }

    public static IEnumerator FadeUIImage(float from, float to, Image image, float rate)
    {
        var direction = to - from;
        direction = Mathf.Sign(direction);

        while (Mathf.Abs(to - from) > .01f)
        {
            from += Time.deltaTime * rate * direction;
            
            var color = MathUtils.GetColorWithAlpha(image.color, from);
            image.color = color;
            yield return null;
        }
    }
    
    public static IEnumerator FadeFloat(float from, float to, float rate, Action<float> setter)
    {
        var direction = to - from;
        direction = Mathf.Sign(direction);

        while (Mathf.Abs(to - from) > .01f)
        {
            from += Time.deltaTime * rate * direction;
            setter?.Invoke(from);
            yield return null;
        }
    }
    
}