/// <summary>
/// 그림자 - 달팽이
/// </summary>
using System;
public class SnailShadow : Shadow
{
    private void Awake()
    {
        stateMachine = new SnailShadowStateMachine(this, animator);
        stateMachine.Init();
    }
}
