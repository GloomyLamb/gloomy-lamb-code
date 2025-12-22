/// <summary>
/// 그림자 - 달팽이
/// </summary>
public class SnailShadow : Shadow
{
    private void Awake()
    {
        stateMachine = new SnailShadowStateMachine(animator);
    }
}
