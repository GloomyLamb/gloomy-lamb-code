using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DuskyPlayer : Player
{
    public Transform SkillPivot => skillPivot;
    [SerializeField] private Transform skillPivot;

    protected override void Init()
    {
    }


    private void Start()
    {
        InputManager.Instance.UseInput(InputType.Player);
    }
}