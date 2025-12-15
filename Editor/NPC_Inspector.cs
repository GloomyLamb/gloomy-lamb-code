using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPC))]
public class NPC_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var npc = (NPC)target;

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
