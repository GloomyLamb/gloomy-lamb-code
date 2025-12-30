using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameManager manager = (GameManager)target;
        base.OnInspectorGUI();

        if (GUILayout.Button("Json 데이터 삭제"))
        {
            manager.Data.ClearSaveData();
        }
    }
}
