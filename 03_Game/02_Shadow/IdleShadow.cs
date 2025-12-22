/// <summary>
/// 그림자 - 기본
/// </summary>
public class IdleShadow : Shadow
{
    private void Awake()
    {
        stateMachine = new IdleShadowStateMachine(animator);
    }
}
