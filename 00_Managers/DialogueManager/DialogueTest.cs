
using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    [SerializeField] DialogueAsset dialogueAsset;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(dialogueAsset.name);
            DialogueManager.Instance.StartDialogue(dialogueAsset);
        }
        else  if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DialogueManager.Instance.StartDialogue(DialogueType.Default, "안녕하세요");
            //DialogueManager.Instance.StartDialogue()
        }
    }
}
