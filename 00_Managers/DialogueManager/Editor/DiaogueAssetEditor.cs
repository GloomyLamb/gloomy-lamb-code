#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueAsset))]
public class DiaogueAssetEditor : Editor
{
    SerializedProperty dialogueOrigin;
    SerializedProperty dialogueData;

    private void OnEnable()
    {
        dialogueOrigin = serializedObject.FindProperty("dialogueOrigin");
        dialogueData = serializedObject.FindProperty("dialogueData");
    }

    // todo : 시간나면 so 데이터를 역으로 csv로 만드는 버튼 넣기
    // 그러러면 파싱한것만 나오게했는데 대사 추가하는 것도 넣어야 함!!

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(dialogueOrigin);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("대사", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        for (int i = 0; i < dialogueData.arraySize; i++)
        {
            SerializedProperty element = dialogueData.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical();

            EditorGUILayout.PropertyField(element, false);

            EditorGUILayout.Space(); //?? 이거 안하면 왜인지 인스펙터에서 마지막 항목이 다른애들보다 좁게 나옴...
            
            // if (i == dialogueData.arraySize - 1)
            // {
            //     //EditorGUILayout.Space();
            //     EditorGUILayout.LabelField("마지막 버튼 (없으면 <닫기>로 나옴", EditorStyles.boldLabel);
            //     EditorGUILayout.PropertyField(element.FindPropertyRelative("buttons"), true);
            // }

            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}


#endif