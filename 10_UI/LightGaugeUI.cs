using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LightGaugeUI : MonoBehaviour
{
    [SerializeField] private BeamSkill _skill;

    [SerializeField] private Image _lightValue;
    [SerializeField] private TextMeshProUGUI _lightValueText;

    private void Update()
    {
        if (_skill != null)
        {
            int lightRatio = (int)(_skill.LightGauge / _skill.MaxLightGauge);
            _lightValue.fillAmount = lightRatio;
            _lightValueText.text = lightRatio.ToString();
        }
    }

    public void ConnectBeamSkill(BeamSkill skill)
    {
        _skill = skill;
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
