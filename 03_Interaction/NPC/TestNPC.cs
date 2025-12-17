public class TestNPC : NPCBase
{
    public override void Interact()
    {
        Logger.Log("NPC와 상호작용");

        DialogueManager.Instance.StartDialogue(DialogueType.Default, "안녕하세요! 저는 테스트 NPC입니다.");
    }
}
