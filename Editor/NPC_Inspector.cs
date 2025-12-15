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
        if (GUILayout.Button("[Test] 말풍선 ON/OFF"))
        {
            if (npc)
            {
                npc.Test_ToggleSpeechBubble();
            }
        }

        if (GUILayout.Button("[Test] 말풍선 교체 - 3D"))
        {
            if (npc)
            {
                npc.Test_SpawnSpeechBubbleDefault();
            }
        }

        if (GUILayout.Button("[Test] 말풍선 교체 - 2D"))
        {
            if (npc)
            {
                npc.Test_SpawnSpeechBubbleUI();
            }
        }
    }
}
