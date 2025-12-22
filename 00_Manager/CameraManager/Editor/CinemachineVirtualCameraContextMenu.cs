using UnityEditor;
using UnityEngine;
using Cinemachine;

/// <summary>
/// CinemachinVirtualCamera에서 뽑아 쓰기
/// </summary>
public static class CinemachineVirtualCameraContextMenu
{
    // todo : CameraManager 에서 Follow, LookAt 은 결국 라이브에서 설정해줄 무언가가 필요함..
    [MenuItem("CONTEXT/CinemachineVirtualCamera/CinemachineInfo SO 생성")]
    private static void ExportCinemacineInfo(MenuCommand command)
    {
        CinemachineVirtualCamera vcam = command.context as CinemachineVirtualCamera;
        if (vcam == null) return;


        CinemachineInfo so = ScriptableObject.CreateInstance<CinemachineInfo>();

        so.cinemachineType = CinemachineType.Virtual;
        so.cameraPos = vcam.transform.position;
        // so.follow = vcam.Follow;
        // so.lookAt = vcam.LookAt;

        so._virtualCam = new CinemachineVirtualInfo();
        so._virtualCam.fieldOfView = vcam.m_Lens.FieldOfView;

        Cinemachine3rdPersonFollow third = vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

        if (third != null)
        {
            so._virtualCam.bodyType = VCBodyType.ThirdPersonFollow;

            so._virtualCam.bodyThridPersonFollow = new Body3PersonFollow();

            so._virtualCam.bodyThridPersonFollow.dampingValue = third.Damping;
            so._virtualCam.bodyThridPersonFollow.shoulderOffset = third.ShoulderOffset;
            so._virtualCam.bodyThridPersonFollow.cameraSide = third.CameraSide;
            so._virtualCam.bodyThridPersonFollow.cameraDistance = third.CameraDistance;
            
            so._virtualCam.bodyThridPersonFollow.cameraCollisionFilter = third.CameraCollisionFilter;
            so._virtualCam.bodyThridPersonFollow.ignoreTag =  third.IgnoreTag;
        }
        else
        {
            CinemachineTransposer transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer != null)
            {
                so._virtualCam.bodyType = VCBodyType.Transposer;

                so._virtualCam.bodyTransposer = new BodyTransposer();

                so._virtualCam.bodyTransposer.bindingMode = transposer.m_BindingMode;
                so._virtualCam.bodyTransposer.followOffset = transposer.m_FollowOffset;
                so._virtualCam.bodyTransposer.xDaming = transposer.m_XDamping;
                so._virtualCam.bodyTransposer.yDaming = transposer.m_YDamping;
                so._virtualCam.bodyTransposer.zDaming = transposer.m_ZDamping;
                so._virtualCam.bodyTransposer.yawDaming = transposer.m_YawDamping;
            }
            else
            {
                so._virtualCam.bodyType = VCBodyType.DoNothing;
            }
        }


        CinemachineComposer composer = vcam.GetCinemachineComponent<CinemachineComposer>();
        if (composer != null)
        {
            so._virtualCam.aimType = VCAimType.Composer;

            so._virtualCam.aimComposer = new AimComposer();

            so._virtualCam.aimComposer.trackedObjectOffset = composer.m_TrackedObjectOffset;
            so._virtualCam.aimComposer.screenX = composer.m_ScreenX;
            so._virtualCam.aimComposer.screenY = composer.m_ScreenY;
            so._virtualCam.aimComposer.deadZoneWidth = composer.m_DeadZoneWidth;
            so._virtualCam.aimComposer.deadZoneHeight = composer.m_DeadZoneHeight;
            so._virtualCam.aimComposer.softZoneWidth = composer.m_SoftZoneWidth;
            so._virtualCam.aimComposer.softZoneHeight = composer.m_SoftZoneHeight;
            so._virtualCam.aimComposer.biasX = composer.m_BiasX;
            so._virtualCam.aimComposer.biasY = composer.m_BiasY;
            so._virtualCam.aimComposer.centerOnActivate = composer.m_CenterOnActivate;
        }
        else
        {
            so._virtualCam.aimType = VCAimType.DoNothing;
        }

        string path = "Assets/20_ScriptableObjects/Camera/newCinemachineInfo.asset";
        AssetDatabase.CreateAsset(so,  AssetDatabase.GenerateUniqueAssetPath(path));
        AssetDatabase.SaveAssets();
        
        AssetDatabase.Refresh();
    }
}