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
    
    public void ExecuteAnimation()
    {
        Debug.Log("exeucte");
        
        for (int i = 0; i < healths.Value; i++)
        {
            Debug.Log(HealthImages[i].rectTransform.anchoredPosition.x);
            
            HealthImages[i].gameObject.SetActive(true);
            StartCoroutine(Coroutines.FadeUIImage(0f, 1f, HealthImages[i], 1f));
        }

        StartCoroutine(
            Coroutines
                .WaitFor(
                    1f / rate + 1f,
                    after: () => After()
                ));

        void After()
        {
            FadeOut(HealthImages[(int) healths.Value - 1]);
            healths.Value--;
            StartCoroutine(Coroutines.WaitFor(1f, after: FadeOutAll));
        }

        void FadeOutAll()
        {
            for (int i = (int) healths.Value - 1; i >= 0; i--)
            {
                Debug.Log(HealthImages[i].rectTransform.anchoredPosition.x);
            
                StartCoroutine(Coroutines.FadeUIImage(1f, 0f, HealthImages[i], 1f));
            }
        }
    }
    
    

    private void LoseLifeAnimation()
    {
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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHealth() 
    {
        
    }
    
    public void FadeIn(Image image)
    {
        StartCoroutine(Coroutines.FadeUIImage(image.color.a, 1f, image, rate));
    }
    
    public void FadeOut(Image image)
    {
        StartCoroutine(Coroutines.FadeUIImage(image.color.a, 0f, image, rate));
    }

    public void FadeOutCurrentHealth()
    {
        var img = HealthImages[(int) healths.Value - 1];
        Debug.Log(img.rectTransform.anchoredPosition.x);
        StartCoroutine(Coroutines.FadeUIImage(img.color.a, 0f, img, rate));

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

        
        if (GUILayout.Button("Fade In"))
        {
            //ui.FadeIn();
        }
        
        if (GUILayout.Button("Fade Out"))
        {
            ui.FadeOutCurrentHealth();
        }
    }
}

#endif
