using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler
{
    protected InputActionAsset inputAsset;

    protected List<(InputAction, Action<InputAction.CallbackContext>)> nowBindingActions;
    protected Dictionary<string, InputActionMap> actionMaps;

    public InputType Type => type;
    private InputType type;

    public InputHandler(InputActionAsset _inputAsset, InputType _type)
    {
        inputAsset = _inputAsset;
        nowBindingActions = new List<(InputAction, Action<InputAction.CallbackContext>)>();
        
        actionMaps = new Dictionary<string, InputActionMap>();
        foreach (InputActionMap map in inputAsset.actionMaps)
        {
            actionMaps.Add(map.name, map);
        }
        
        type = _type;
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

    public bool IsPressed(InputMapName mapName, InputActionName actionName)
    {
        string mapNameString = mapName.ToString();
        string actionNameString = actionName.ToString();

        if (actionMaps.TryGetValue(mapNameString, out InputActionMap map))
        {
            InputAction inputAction = map.actions.FirstOrDefault(a => a.name == actionNameString);
            return inputAction.IsPressed();
        }
        return false;
    }

    public InputAction GetInputAction(InputMapName mapName, InputActionName actionName)
    {
        string mapNameString = mapName.ToString();
        string actionNameString = actionName.ToString();

        if (actionMaps.TryGetValue(mapNameString, out InputActionMap map))
        {
            InputAction inputAction = map.actions.FirstOrDefault(a => a.name == actionNameString);
            return inputAction;
        }
        return null;
    }

    public Vector2 GetAxis(InputMapName mapName, InputActionName actionName)
    {
        InputAction inputAction = GetInputAction(mapName, actionName);
        if (inputAction != null)
        {
            return inputAction.ReadValue<Vector2>();
        }
        return Vector2.zero;
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
