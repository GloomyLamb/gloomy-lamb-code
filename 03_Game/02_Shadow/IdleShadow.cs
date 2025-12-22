public class IdleShadow : Shadow
{
    private void Awake()
    {
        stateMachine = new IdleShadowStateMachine(animator);
    }
}
