using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLimit : SceneSingletonManager<AreaLimit>
{
    public float LimitDistance => _limitDistance;
    private float _limitDistance;
    private Vector3 thisPosXZ;

    [SerializeField] private Renderer _renderer;
    

    private MaterialPropertyBlock _materialProperty;

    private float _blockAlphaValue = 0.03f;
    private float _currentAlphaValue = 0f;
    [SerializeField] private float _fadeSpeed = 1f;
    
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        _limitDistance = this.transform.lossyScale.x / 2;
        thisPosXZ = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
        
        
        _materialProperty = new MaterialPropertyBlock();
    }

    private void Update()
    {

        if (_currentAlphaValue > 0)
        {
            _currentAlphaValue -= (Time.deltaTime * _fadeSpeed);
            if(_currentAlphaValue <= 0) _currentAlphaValue = 0;
            
            _renderer.GetPropertyBlock(_materialProperty);
            _materialProperty.SetFloat("_BaseAlpha",_currentAlphaValue);
            _renderer.SetPropertyBlock(_materialProperty);
        }
    }

    public bool CanMove(Vector3 nextPosition)
    {
        Vector3 nextPosXZ = new Vector3(nextPosition.x, 0f, nextPosition.z);
        Debug.Log(Vector3.Distance(thisPosXZ, nextPosXZ));
        if (Vector3.Distance(thisPosXZ, nextPosXZ) >= _limitDistance)
        {
            _renderer.GetPropertyBlock(_materialProperty);
            _materialProperty.SetFloat("_BaseAlpha", _blockAlphaValue);
            _renderer.SetPropertyBlock(_materialProperty);
            _currentAlphaValue = _blockAlphaValue;
            return false;
        }
        
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, LimitDistance);
    }
}
