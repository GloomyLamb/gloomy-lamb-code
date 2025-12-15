using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueAsset", menuName = "SO/Dialogue Asset")]
public class DialogueAsset : ScriptableObject
{
    [SerializeField] private TextAsset dialogueOrigin;
    [SerializeField] private List<DialogueData> dialogueData;

    void Parse()
    {
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (dialogueOrigin == null) return;
        Parse();
    }
#endif
}