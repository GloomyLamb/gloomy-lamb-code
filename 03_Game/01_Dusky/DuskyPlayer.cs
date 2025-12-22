using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DuskyPlayer : Player
{
    // 어디에 달고 Skill 들이 어떻게 알고 놔줄지?
    public Transform SkillPivot => skillPivot;
    [SerializeField] private Transform skillPivot;
    
    
    protected override void Init()
    {
        _stateMachine = new DuskyStateMachine(_animator);
    }

    private void Update()
    {
        
    }

    private void Start()
    {
        InputManager.Instance.UseInput(InputType.Player);
    }
    
}