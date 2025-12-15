using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(DialogueData))]
public class DialogueDataPropertyDrawer : PropertyDrawer
{
    private const int DialogueLineCount = 3;
 
    // Inspector에서 잘 볼 수 있게...
    // todo : 나중에 역으로 뽑을 수도 있게 버튼같은거 추가해야함
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty name = property.FindPropertyRelative("name");
        SerializedProperty sprName = property.FindPropertyRelative("sprName");
        SerializedProperty emotion = property.FindPropertyRelative("emotion");
        SerializedProperty dialogue = property.FindPropertyRelative("dialogue");
        SerializedProperty buttons = property.FindPropertyRelative("buttons");

        float line = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        float y = position.y;
        Rect top = new Rect(position.x, y, position.width, line);

        float widthRate = 0.32f;
        float nameWidth = position.width * widthRate;
        float sprWidth = position.width * widthRate;
        float emotionWidth = position.width * widthRate;

        // 위에 이름이랑 스프라이트 같이 놓기
        EditorGUI.PropertyField(new Rect(top.x, y, nameWidth, line), name, GUIContent.none);
        EditorGUI.PropertyField(new Rect(top.x + nameWidth + 5, y, sprWidth, line), sprName, GUIContent.none);
        EditorGUI.PropertyField(new Rect(top.x + nameWidth + sprWidth + 10, y, emotionWidth, line), emotion, GUIContent.none);

        y += (line + spacing);

        // 대사
        EditorGUI.PropertyField(new Rect(top.x, y, position.width, line * DialogueLineCount), dialogue, GUIContent.none);

        y += (line * DialogueLineCount) + spacing;

        
        // 버튼
        if (buttons != null)
        {
            for (int i = 0; i < buttons.arraySize; i++)
            {
                SerializedProperty buttonElement = buttons.GetArrayElementAtIndex(i);
                SerializedProperty buttonDesc = buttonElement.FindPropertyRelative("buttonDescriptions");
                
                //EditorGUILayout.BeginVertical()
        
                float rowHeight = line + spacing;
                float labelWidth = 50f;
                
                Rect rowRect = new Rect(position.x, y, position.width, rowHeight);
                
                EditorGUI.LabelField(new Rect(rowRect.x, rowRect.y, labelWidth, rowHeight), $"버튼 {i}");
                
                float positionX = rowRect.x + labelWidth + 5f;
                float buttonWidth = rowRect.width - labelWidth - 5f;
                
                SerializedProperty type = buttonElement.FindPropertyRelative("type");
                if ((DialogueButtonType)type.enumValueIndex == DialogueButtonType.NextDialogue)
                {
                    float half = (buttonWidth - 5f) * 0.5f;
                
                    Rect descRect = new Rect(positionX, rowRect.y, half, rowHeight);
                    Rect assetRect = new Rect(positionX + half + 5f, rowRect.y, half, rowHeight);
                
                    EditorGUI.PropertyField(descRect, buttonDesc, GUIContent.none);
                
                    SerializedProperty nextAsset = buttonElement.FindPropertyRelative("nextDialogueAssets");
                
                    EditorGUI.PropertyField(assetRect, nextAsset, GUIContent.none);
                }
                else
                {
                    Rect descRect = new Rect(positionX, rowRect.y, buttonWidth , rowHeight);
                    EditorGUI.PropertyField(descRect, buttonDesc, GUIContent.none);
                }
                

                //EditorGUI.PropertyField(left, buttonDesc, GUIContent.none);
                //
                
                y += rowHeight + spacing;
            }
        }


        // EditorGUI.PropertyField(new Rect(top.x, top.y + (line * 4) + (spacing * 2), position.width, line), buttons,
        //     GUIContent.none);

        // Rect dialogueRect = new Rect(position.x, position.y + line + spacing, position.width, position.height - line - spacing);
        // dialogue.stringValue = EditorGUI.TextArea(dialogueRect, dialogue.stringValue);

        EditorGUI.EndProperty();
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty buttons = property.FindPropertyRelative("buttons");
        
        float line = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        float height = line + spacing + (line * DialogueLineCount) + spacing;
        
        if (buttons != null)
        {
            float rowHeight = line + spacing * 2;
            height += buttons.arraySize * (rowHeight + spacing);
        }

        return height;
        // return line;
        //return (line * preopertyCount) + (spacing * (preopertyCount - 1));
    }
}

#endif