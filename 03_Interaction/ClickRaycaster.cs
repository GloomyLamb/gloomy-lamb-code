using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// NPC 클릭 레이캐스터
/// </summary>
public class ClickRaycaster : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionAsset _inputActions;

    [Header("Ray")]
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private LayerMask _layerMask;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        InputManager.Instance.BindInputEvent(
            InputType.Camera,
            InputMapName.Default,
            InputActionName.Interaction,
            OnClick);
        InputManager.Instance.UseInput(InputType.Camera);
    }


    /// <summary>
    /// input handler - camera의 click 이벤트 콜백
    /// </summary>
    /// <param name="context"></param>
    private void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Logger.Log("마우스 클릭");
            if (_camera == null) return;

            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = _camera.ScreenPointToRay(mousePos);
            Debug.DrawRay(ray.origin, ray.direction * _maxDistance, Color.red, 1f);

            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _layerMask))
            {
                if (hit.collider.TryGetComponent<BaseNPC>(out var npc))
                {
                    npc.Interact();
                }
            }
        }
    }

    #region 에디터 전용
#if UNITY_EDITOR
    private void Reset()
    {
        if (_inputActions == null)
            _inputActions = FindInputActionAsset("InputAction_Camera");

        _layerMask = LayerMask.GetMask("Interactable");
    }

    private InputActionAsset FindInputActionAsset(string name)
    {
        Logger.Log($"InputActionAsset 찾기: {name}");

        string[] guids = AssetDatabase.FindAssets($"t:InputActionAsset {name}");
        if (guids.Length == 0)
        {
            Logger.LogWarning($"InputActionAsset 못 찾음: {name}");
            return null;
        }

        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        return AssetDatabase.LoadAssetAtPath<InputActionAsset>(path);
    }
#endif
    #endregion
}
