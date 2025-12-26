using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
public class BeamController : MonoBehaviour
{
    [Header("References")]
    public Transform cornTransform;
    public Light spotLight;
    
    [Header("Beam parmeters (Tune in Inspector)")]
    [Tooltip("빔이 확장되어 나아가는 속도")]
    public float expandSpeed;

    [Tooltip("빔의 최대 길이(Scale의 Z값)" )]
    public float maxLength;

    [Tooltip("빔의 범위폭(Scale의X,Y값")]
    public float maxWidth;

    [Tooltip("빔의 시작 폭(Scale의 X,Y값)")]
    public float startWidth;
    [Header("Light 의 밝기")]
    public float maxIntensity;

    private bool isExpanding;
    private float currentLength;

    [Header("Spot Light Settings")]
    public float startSpotAngle;
    public float maxSpotAngle;
    public float startIntensity;
    
    
    List<IDamageable> _damageables = new List<IDamageable>();
    Coroutine _damageRoutine = null;
    BeamSkillData _beamData;

    public void Init(BeamSkillData beamData)
    {
        _beamData = beamData;        
    }
    
    private void Start()
    {
        SetEnabled(false);
        ApplyBeam(0f);
    }

    private void OnEnable()
    {

    }

    private void Update()
    {
      
        if (!isExpanding) return;

        currentLength += expandSpeed * Time.deltaTime;

        if(currentLength >= maxLength)
        {
            currentLength = maxLength;
            isExpanding = false;   //빛이 최대 길이에 도달하면 확장, 켜진상태 유지 
        }

        ApplyBeam(currentLength); 
    }

    public void SetEnabled(bool on)
    {
        if (cornTransform != null)
            cornTransform.gameObject.SetActive(on);

        if (spotLight != null)
            spotLight.enabled = on;

        if (on)
        {
            if(_damageRoutine != null)
                StopCoroutine(_damageRoutine);
            _damageRoutine = StartCoroutine(LightDamageRoutine());
            _damageables.Clear();
        }
        else
        {
            foreach (IDamageable damageable in _damageables)
            {
                damageable.StopEffect(); 
            }
            _damageables.Clear();
        
            if(_damageRoutine != null)
                StopCoroutine(_damageRoutine);
        }
    }

    void ApplyBeam(float length) 
    {
        if (cornTransform == null) return;  //cornTransform 이 연결

        float t = (maxLength <= 0f) ? 0f : Mathf.Clamp01(length / maxLength);  

        float visibleLength = Mathf.Max(length, 0.5f);
        float widthXZ = Mathf.Lerp(startWidth, maxWidth, t);
        widthXZ = Mathf.Max(widthXZ, 0.5f);

        cornTransform.localScale = new Vector3(widthXZ, widthXZ, visibleLength);

        if (spotLight != null)
        {
            
            spotLight.range = visibleLength;
         // spotLight.spotAngle = Mathf.Lerp(startSpotAngle, maxSpotAngle, t);
            spotLight.intensity = Mathf.Lerp(startIntensity, maxIntensity, t);
            
            
        }
    }
    public void PlayBeam()
    {

        SetEnabled(true);
        currentLength = 0f;
        isExpanding = true;
        ApplyBeam(0f);
        
    }

    public void OnTriggerEnter(Collider other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        
        if (hit != null)
        {
            if (_damageables.Contains(hit) == false)
            {
                _damageables.Add(hit);
                hit.ApplyEffect();
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit != null)
        {
            if (_damageables.Contains(hit) == true)
            {
                hit.StopEffect();
                _damageables.Remove(hit);
            }
        }
    }

    private void OnDisable()
    {
    
    }

    IEnumerator LightDamageRoutine()
    {
        WaitForSeconds tick = new  WaitForSeconds(_beamData.DamageTickSec);
        while (true)
        {
            yield return tick;
                Debug.Log("안녕ㅇㅇㅇ");
            foreach (IDamageable damageable in _damageables)
            {
                damageable.Damage(_beamData.AttackDamage);
            }
        }
    }
}


    






