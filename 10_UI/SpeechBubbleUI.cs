using UnityEngine;

public class SpeechBubbleUI : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 camForward = Camera.main.transform.forward;
        transform.forward = new Vector3(camForward.x, 0, camForward.z);
    }
}
