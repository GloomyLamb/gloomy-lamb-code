using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler
{
    protected InputActionAsset inputAsset;
    public bool Enabled => inputAsset == null ? false : inputAsset.enabled;

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
            inputAsset.Enable();
        }
        else if (enable == false)
        {
            inputAsset.Disable();
        }
    }

    public void SetEnableInput(InputMapName mapName, InputActionName actionName, bool enable)
    {
        string mapNameString = mapName.ToString();
        string actionNameString = actionName.ToString();

        if (actionMaps.TryGetValue(mapNameString, out InputActionMap map))
        {
            InputAction inputAction = map.actions.FirstOrDefault(a => a.name == actionNameString);
            
            if (inputAction == null) return;
            
            if(enable)
                inputAction.Enable();
            else
                inputAction.Disable();
        }

    }

    public bool IsPressed(InputActionName actionName, InputMapName mapName = InputMapName.Default)
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

    public InputAction GetInputAction(InputActionName actionName, InputMapName mapName = InputMapName.Default)
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

    public Vector2 GetAxis(InputActionName actionName, InputMapName mapName = InputMapName.Default)
    {
        InputAction inputAction = GetInputAction(actionName, mapName);
        if (inputAction != null)
        {
            return inputAction.ReadValue<Vector2>();
        }

        return Vector2.zero;
    }

    public float GetFloat(InputActionName actionName, InputMapName mapName = InputMapName.Default)
    {
        InputAction inputAction = GetInputAction(actionName, mapName);
        if (inputAction != null)
        {
            return inputAction.ReadValue<float>();
        }

        return 0f;
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