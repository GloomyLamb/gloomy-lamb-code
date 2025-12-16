using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// NPC 클릭 레이캐스터
/// </summary>
public class ClickRaycaster : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionAsset _inputActions;
    private InputHandler _input;

    [Header("Ray")]
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private LayerMask _layerMask;
    private Camera _camera;

    private void Awake()
    {
        Init();
        _camera = Camera.main;
    }

    private void Init()
    {
        _input = new InputHandler(_inputActions, InputType.Camera);
        _input.BindInputEvent(InputMapName.Default, InputActionName.Interaction, OnClick);
    }

    private void Update()
    {
        //Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * _maxDistance, Color.red, 1f);
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Logger.Log("마우스 클릭");
            if (_camera == null) return;

            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = _camera.ScreenPointToRay(mousePos);
            Debug.DrawRay(ray.origin, ray.direction * _maxDistance, Color.red, 1f);

            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _layerMask))
            {
                if (hit.collider.TryGetComponent<NPCBase>(out var npc))
                {
                    npc.Interact();
                }
            }
        }
    }
}
