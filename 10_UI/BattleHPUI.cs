using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
public class BattleHPUI : MonoBehaviour
{
    private Player player;
    private ShadowController shadowController;
    [SerializeField] private Image HpFill;
    [SerializeField] private Image bossFill;
    private float lerpSpeed = 5f;
    private float playerCurrentFill;
    private float bossCurrentFill;
    // Start is called before the first frame update

    private float testDamage = 10f;
    // Update is called once per frame

    private void Awake()
    {
        if (player == null) player = FindObjectOfType<Player>();
        if (shadowController == null) shadowController = FindObjectOfType<ShadowController>();
        if (HpFill == null) HpFill = GetComponentInChildren<Image>(); // 필요시 직접 지정 권장
        if (bossFill == null) bossFill = GetComponentInChildren<Image>();
    }
    private void Start()
    {
        if (player != null && player.Status != null && player.Status.MaxHp > 0f)
        {
            playerCurrentFill = player.Status.Hp / player.Status.MaxHp;
            HpFill.fillAmount = playerCurrentFill;
        }
        if (shadowController != null && shadowController.Status != null && shadowController.Status.MaxHp > 0f && bossFill != null)
        {
            bossCurrentFill = shadowController.Status.Hp / shadowController.Status.MaxHp;
            bossFill.fillAmount = bossCurrentFill;
        }

    }
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.T))
        {
          
            player.Damage(testDamage);

          

            Debug.Log($"[TEST] T pressed. Damage={testDamage}, HP={player.Status.Hp}/{player.Status.MaxHp}");
        }

        float maxHp = player.Status.MaxHp;
        if (maxHp <= 0f) return;

        float targetFill = player.Status.Hp / maxHp;

        
        

        if (Input.GetKeyDown(KeyCode.Y) && shadowController != null)
        {
             shadowController.Damage(testDamage); // ShadowController.Damage 사용
            Debug.Log($"[TEST] Boss HP={shadowController.Status.Hp}/{shadowController.Status.MaxHp}");
        }

        // Player UI
        if (player != null && player.Status != null && player.Status.MaxHp > 0f && HpFill != null)
        {
            float target = player.Status.Hp / player.Status.MaxHp;
            playerCurrentFill = Mathf.Lerp(playerCurrentFill, target, Time.deltaTime * lerpSpeed);
            HpFill.fillAmount = playerCurrentFill;
        }

        // Boss UI
        if (shadowController != null && shadowController.Status != null && shadowController.Status.MaxHp > 0f && bossFill != null)
        {
            float target = shadowController.Status.Hp / shadowController.Status.MaxHp;
            bossCurrentFill = Mathf.Lerp(bossCurrentFill, target, Time.deltaTime * lerpSpeed);
            bossFill.fillAmount = bossCurrentFill;
        }
    }
        
}
