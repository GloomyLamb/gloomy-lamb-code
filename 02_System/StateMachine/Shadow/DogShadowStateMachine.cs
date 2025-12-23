using UnityEngine;

public class DogShadowStateMachine : ShadowStateMachine
{
    public DogShadow DogShadow { get; private set; }

    // Ground

    // Skill

    // Battle

    public DogShadowStateMachine(Shadow shadow, Animator animator) : base(shadow, animator)
    {
        DogShadow = shadow as DogShadow;


    }
}
