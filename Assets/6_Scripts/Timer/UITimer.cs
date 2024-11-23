using _6_Scripts.Utils.DataFormating;
using TMPro;
using UnityEngine;

namespace _6_Scripts.Timer
{
    public class UITimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private FloatVariable minutes;
        [SerializeField] private FloatVariable seconds;

        private float elapsed = 0f;
        
        private void Awake()
        {
            // gm = this
            GameManager.instance.UITimer = this;
        }

        private void Start()
        {
            UpdateUI();
        }

        private void Update()
        {
            elapsed += Time.deltaTime;

            if (elapsed >= 1f)
            {
                elapsed = 0f;
                UpdateUI();
            }
        }

        public void UpdateUI()
        {
            var min = DataFormating.FormatIntData((int) minutes.Value, out var minutesCount);
            var sec = DataFormating.FormatIntData((int) seconds.Value, out var secondsCount);

            if (secondsCount <= 1)
            {
                sec = sec.Insert(0, "<sprite=0>");
            }

            if (minutesCount <= 1)
            {
                min = min.Insert(0, "<sprite=0>");
            }
            
            timerText.text = $"{min}<sprite=10>{sec}";
        }

        public void Load(IntSaveData data)
        {
            minutes.Value = data.Value / 60;
            seconds.Value = data.Value % 60;
            
            UpdateUI();
        }
    }
}