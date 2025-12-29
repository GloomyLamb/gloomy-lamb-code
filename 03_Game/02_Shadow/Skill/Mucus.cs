using UnityEngine;

public class Mucus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)   // is Trigger 체크박스가 켜져 있어야 작동 트리거체크 
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            SoundManager.Instance.PlaySfxOnce(SfxName.Slime, idx: 1);
            player.TakeSlowDown();
        }
    }
}
