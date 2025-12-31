using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryScene : BaseScene
{
    private void Start()
    {
        InputManager.Instance?.LockInput(InputType.Player,InputMapName.Default,InputActionName.Attack);
    }
}
