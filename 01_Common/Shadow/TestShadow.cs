using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestShadow : MonoBehaviour, IDamageable
{
    // 수치
    public float health = 100f;
    public float maxHealth = 100f;

    // UI 연동
    public Image healthBar;
    public TextMeshProUGUI healthText;

    private void Update()
    {
        healthBar.fillAmount = health / maxHealth;
        healthText.text = $"{health} / {maxHealth}";
    }

    public void Damage(float damage)
    {
        Logger.Log($"{damage}의 대미지");

        health = Mathf.Max(health - damage, 0);

        if (health == 0)
        {
            Logger.Log("피 0됨");
            health = maxHealth;
        }
    }

    public void ApplyEffect()
    {
    }
}
