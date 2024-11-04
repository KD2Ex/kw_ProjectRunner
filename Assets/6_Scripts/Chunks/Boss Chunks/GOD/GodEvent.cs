using UnityEngine;

public class GodEvent : EventAppearance
{
    [SerializeField] private GodOnLocation god;
    [SerializeField] private CameraSlide cameraSlide;
    
    public override void Appear()
    {
        base.Appear();
        god.gameObject.SetActive(true);
        cameraSlide.Appear();
    }

    public override void Disappear()
    {
        base.Disappear();
        god.Disappear();
        cameraSlide.Disappear();
    }
}
