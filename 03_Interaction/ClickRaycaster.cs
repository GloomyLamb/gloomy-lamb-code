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

    private void OnClick(InputAction.CallbackContext context)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _layerMask))
        {

        }
    }
}
