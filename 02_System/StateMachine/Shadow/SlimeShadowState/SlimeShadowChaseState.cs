public class SlimeShadowChaseState : SlimeShadowState
{
    public SlimeShadowChaseState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    //    protected override IEnumerator StateCoroutine()
    //    {
    //        SoundManager.Instance.PlaySfxOnce(SfxName.Slime, idx: 2);

    //        if (!SlimeShadow.IsFastMode)
    //        {
    //            SlimeShadow.SetMovementMultiplier(MovementType.Walk);
    //            yield return new WaitForSeconds(SlimeShadow.SlowChasePatternTime);
    //            SlimeShadow.IsFastMode = true;
    //        }
    //        else
    //        {
    //            yield return new WaitForSeconds(SlimeShadow.FastChasePatternTime);
    //        }

    //        SlimeShadow.PlusChaseCount();
    //        stateMachine.ChangeState(stateMachine.IdleState);
    //    }
}
