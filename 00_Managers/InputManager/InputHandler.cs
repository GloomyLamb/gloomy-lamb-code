using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

public abstract class InputHandler
{
    protected InputActionAsset inputAsset;

    protected List<(InputAction, Action<InputAction.CallbackContext>)> nowBindingActions;
    protected Dictionary<string, InputActionMap> actionMaps;

    public bool UseCursor => useCursor;
    private bool useCursor = false;

    public InputHandler(InputActionAsset _inputAsset, bool _useCursor = true)
    {
        inputAsset = _inputAsset;
        nowBindingActions = new List<(InputAction, Action<InputAction.CallbackContext>)>();
        actionMaps = new Dictionary<string, InputActionMap>();

        useCursor = _useCursor;
    }

    public void SetEnableInput(bool enable)
    {
        if (enable == true)
        {
            Logger.Log("Enable Input");
            inputAsset.Enable();
        }
        else if (enable == false)
        {
            inputAsset.Disable();
        }
    }

    public void BindInputEvent(InputMapName mapName, InputActionName actionName, Action<InputAction.CallbackContext> action)
    {
        string mapNameString = mapName.ToString();
        string actionNameString = actionName.ToString();

        
        if (actionMaps.TryGetValue(mapNameString, out InputActionMap map))
        {
            InputAction inputAction = map.actions.FirstOrDefault(a => a.name == actionNameString);

            if (inputAction == null) return;
            
            inputAction.started += action;
            inputAction.performed += action;
            inputAction.canceled += action;
            
            nowBindingActions.Add((inputAction, action));
        }
    }
    
    
    public void DisposeInputEvent()
    {
        for (int i = nowBindingActions.Count - 1; i >= 0; i--)
        {
            nowBindingActions[i].Item1.started -= nowBindingActions[i].Item2;
            nowBindingActions[i].Item1.performed -= nowBindingActions[i].Item2;
            nowBindingActions[i].Item1.canceled -= nowBindingActions[i].Item2;
            nowBindingActions.Remove(nowBindingActions[i]);
        }
    }


}
