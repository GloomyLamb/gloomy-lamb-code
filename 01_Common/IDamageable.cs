public interface IDamageable
{
    public void Damage(float damage);
    public void ApplyEffect(IAttackable attackable)
    {
        attackable?.GiveEffect();
    }
}
