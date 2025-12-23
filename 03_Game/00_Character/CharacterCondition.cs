using System;

[Flags]
public enum CharacterCondition
{
    None = 0,
    Invincible= 1 << 2, // 무적
    Stun = 1 << 3,      //스턴
}