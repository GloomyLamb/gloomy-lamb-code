using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LightGaugeUI : MonoBehaviour
{
    [Header("Skill")]
     private BeamSkill _skill;

    [Header("UI")]
    [SerializeField] private Image _lightValue;               // Filled Image (Radial 360)
    [SerializeField] private TextMeshProUGUI _lightValueText;  // TMP Text

    [Header("Display")]
    [SerializeField] private bool _showPercent = true;         // 33%
    [SerializeField] private bool _showSlashValue = false;     // 33/100
    [SerializeField] private float _smoothSpeed = 12f;         // smoothing speed
    private float _displayFill;
    private void Awake()
    {
        InitDisplayFromSkill();
    }

    private void Update()
    {
        if (_skill == null) return;
        if (_lightValue == null || _lightValueText == null) return;

        float max = Mathf.Max(0.01f, _skill.maxLightGauge);
        float targetFill = Mathf.Clamp01(_skill.lightGauge / max);

        // 부드러운 보간
        _displayFill = Mathf.Lerp(_displayFill, targetFill, 1f - Mathf.Exp(-_smoothSpeed * Time.deltaTime));

        // 1) 링 채우기
        _lightValue.fillAmount = _displayFill;

        // 2) 텍스트
        if (_showSlashValue)
        {
            int cur = Mathf.RoundToInt(_displayFill * max);
            int m = Mathf.RoundToInt(max);
            _lightValueText.text = $"{cur}/{m}";
        }
        else if (_showPercent)
        {
            int percent = Mathf.RoundToInt(_displayFill * 100f);
            _lightValueText.text = percent + "%";
        }
        else
        {
            _lightValueText.text = Mathf.RoundToInt(_displayFill * max).ToString();
        }
    }

    public void ConnectBeamSkill(BeamSkill skill)
    {
        _skill = skill;
        InitDisplayFromSkill();
    }

    private void InitDisplayFromSkill()
    {
        if (_skill == null) return;

        float max = Mathf.Max(0.01f, _skill.maxLightGauge);
        float ratio = Mathf.Clamp01(_skill.lightGauge / max);
        _displayFill = ratio;

        if (_lightValue != null) _lightValue.fillAmount = ratio;
        if (_lightValueText != null)
        {
            if (_showSlashValue)
                _lightValueText.text = $"{Mathf.RoundToInt(ratio * max)}/{Mathf.RoundToInt(max)}";
            else if (_showPercent)
                _lightValueText.text = Mathf.RoundToInt(ratio * 100f) + "%";
            else
                _lightValueText.text = Mathf.RoundToInt(ratio * max).ToString();
        }
    }


    #region 에디터 전용
#if UNITY_EDITOR
    private void Reset()
    {
        _lightValue = transform.FindChild<Image>("Image - LightValue");
        _lightValueText = transform.FindChild<TextMeshProUGUI>("Text (TMP) - LightValue");
    }
#endif
    #endregion
}
