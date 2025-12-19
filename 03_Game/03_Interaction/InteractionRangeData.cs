using UnityEngine;

[CreateAssetMenu(fileName = "new Interaction Range Data", menuName = "SO/Interaction Range Data")]
public class InteractionRangeData : ScriptableObject
{
    [field: Header("Interaction Setting")]
    [field: SerializeField] public float InteractRange { get; private set; } = 3f;
    [field: SerializeField][field: Range(0f, 180f)] public float InteractAngle { get; private set; } = 120f;
    [field: SerializeField] public LayerMask InteractableLayer { get; private set; }
    [field: SerializeField] public bool UseAngleWeight { get; private set; } = false;
    [field: SerializeField] public bool UseDistanceWieght { get; private set; } = true;
}
