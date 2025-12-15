using UnityEditor;
using UnityEngine;

public abstract class NPC_Inspector<T> : Editor where T : NPC
{
    protected T npc;
    protected virtual void OnEnable()
    {
        npc = (T)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawTestButtons();
    }

    protected virtual void DrawTestButtons()
    {
        EditorGUILayout.LabelField("테스트 버튼", EditorStyles.boldLabel);

        if (GUILayout.Button("말풍선 ON / OFF"))
        {
            Undo.RecordObject(npc.gameObject, "Toggle Speech Bubble");
            npc.Test_ToggleSpeechBubble();
            EditorUtility.SetDirty(npc);
        }

        if (GUILayout.Button("말풍선 교체 - 3D"))
        {
            Undo.RecordObject(npc.gameObject, "Spawn Speech Bubble 3D");
            npc.Test_SpawnSpeechBubbleDefault();
            EditorUtility.SetDirty(npc);
        }

        if (GUILayout.Button("말풍선 교체 - 2D"))
        {
            Undo.RecordObject(npc.gameObject, "Spawn Speech Bubble UI");
            npc.Test_SpawnSpeechBubbleUI();
            EditorUtility.SetDirty(npc);
        }
    }
}
