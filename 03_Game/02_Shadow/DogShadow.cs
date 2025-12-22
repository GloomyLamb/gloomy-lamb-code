/// <summary>
/// 그림자 - 개
/// </summary>
public class DogShadow : Shadow
{
    private void Awake()
    {
        stateMachine = new DogShadowStateMachine(animator);
    }
}
