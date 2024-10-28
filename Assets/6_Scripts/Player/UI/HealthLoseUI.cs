using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthLoseUI : MonoBehaviour
{
    [SerializeField] private FloatVariable healths;
    [SerializeField] private float rate;

    [SerializeField] private Image uiPrefab;
    [SerializeField] private int maxCount;

    private List<Image> HealthImages = new();

    private Coroutine coroutine;

    private int healthCount => (int) healths.Value + 1;
    
    public void ExecuteAnimation()
    {
        Debug.Log("exeucte");
        
        for (int i = 0; i < healthCount; i++)
        {
            Debug.Log(HealthImages[i].rectTransform.anchoredPosition.x);
            
            HealthImages[i].gameObject.SetActive(true);
            FadeIn(HealthImages[i]);
        }

        coroutine = StartCoroutine(
            Coroutines
                .WaitFor(
                    1f / rate + 1f,
                    before: () =>
                    {
                        Debug.Log($"Before coroutine log: {coroutine}");
                        
                    },
                    after: () => After()
                ));

        void After()
        {
            FadeOut(HealthImages[healthCount - 1]);
            coroutine = null;
            StartCoroutine(Coroutines.WaitFor(1f, after: FadeOutAll));
        }

        void FadeOutAll()
        {
            for (int i = healthCount - 2; i >= 0; i--)
            {
                Debug.Log(HealthImages[i].rectTransform.anchoredPosition.x);
            
                StartCoroutine(Coroutines.FadeUIImage(1f, 0f, HealthImages[i], 1f));
            }
        }
    }
    
    private void Awake()
    {
        var originPos = new Vector3(600f, 35f, 0f);
        
        for (int i = 0; i < maxCount; i++)
        {
            var inst = Instantiate(uiPrefab, transform);
            inst.rectTransform.localPosition = originPos;

            
            inst.gameObject.SetActive(false);
            HealthImages.Add(inst);
            originPos.x += -100f;
        }
    }
    
    void Update()
    {
        Debug.Log($"Coroutine: {coroutine}");
    }
    
    private void FadeIn(Image image)
    {
        StartCoroutine(Coroutines.FadeUIImage(image.color.a, 1f, image, rate));
    }
    
    private void FadeOut(Image image)
    {
        StartCoroutine(Coroutines.FadeUIImage(image.color.a, 0f, image, rate));
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(HealthLoseUI))]
public class HealthLoseUIInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        HealthLoseUI ui = target as HealthLoseUI;

        EditorGUILayout.Space(10f);
        
        if (GUILayout.Button("Execute Animation"))
        {
            ui.ExecuteAnimation();
        }
    }
}

#endif
