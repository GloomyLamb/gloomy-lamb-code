using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraManager))]
public class CameraManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var cameraManager = (CameraManager)target;

        if (GUILayout.Button("[Test] 리스트 초기화"))
        {
            if (cameraManager)
            {
                cameraManager.Test_ResetCamera();
            }
        }

        if (GUILayout.Button("[Test] 카메라 넣기"))
        {
            if (cameraManager)
            {
                cameraManager.Test_PushCamera();
            }
        }

        if (GUILayout.Button("[Test] 카메라 1번"))
        {
            if (cameraManager)
            {
                cameraManager.Test_SwitchCamera1();
            }
        }

        if (GUILayout.Button("[Test] 카메라 2번"))
        {
            if (cameraManager)
            {
                cameraManager.Test_SwitchCamera2();
            }
        }

        if (GUILayout.Button("[Test] 카메라 3번"))
        {
            if (cameraManager)
            {
                cameraManager.Test_SwitchCamera3();
            }
        }

        if (GUILayout.Button("[Test] 외부 카메라 전환"))
        {
            if (cameraManager)
            {
                cameraManager.Test_ExternalCamera();
            }
        }
    }
}
