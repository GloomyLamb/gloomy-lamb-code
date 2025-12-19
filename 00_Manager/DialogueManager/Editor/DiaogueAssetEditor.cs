#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueAsset))]
public class DiaogueAssetEditor : Editor
{
    SerializedProperty dialogueOrigin;
    SerializedProperty dialogueList;
    SerializedProperty dialogueType;

    private void OnEnable()
    {
        dialogueOrigin = serializedObject.FindProperty("dialogueOrigin");
        dialogueList = serializedObject.FindProperty("dialogueList");
        dialogueType = serializedObject.FindProperty("dialogueType");
    }

    // todo : 시간나면 so 데이터를 역으로 csv로 만드는 버튼 넣기
    // 그러러면 파싱한것만 나오게했는데 대사 추가하는 것도 넣어야 함!!
    public override void OnInspectorGUI() 
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(dialogueOrigin);

        if (GUILayout.Button("읽기"))
        {
            DialogueAsset targetObj = (DialogueAsset)target;
            targetObj.Parse();
        }
        
        if (GUILayout.Button("덮어쓰기"))
        {
            Logger.Log("아직 없지요~");
            // DialogueAsset targetObj = (DialogueAsset)target;
            // targetObj.RemoveDialogueData();
        }
        
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("[대화 타입]", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(dialogueType);
        
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("[대사 목록]", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        for (int i = 0; i < dialogueList.arraySize; i++)
        {
            SerializedProperty element = dialogueList.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField(i.ToString("00"), EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(element, false);

            EditorGUILayout.Space(); //?? 이거 안하면 왜인지 인스펙터에서 마지막 항목이 다른애들보다 좁게 나옴...
            
            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}


#endif