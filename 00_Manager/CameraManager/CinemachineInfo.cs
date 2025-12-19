using Cinemachine;
using UnityEngine;
using static Cinemachine.LensSettings;

public enum CinemachineType
{
    Virtual,
    FreeLook,
}

public enum VCBodyType
{
    DoNothing,
    ThirdPersonFollow,
    Transposer
}

public enum VCAimType
{
    DoNothing,
    Composer,
    HardLookAt
}

[CreateAssetMenu(fileName = "new cinemachine info", menuName = "SO/cinemachine camera")]
public class CinemachineInfo : ScriptableObject
{
    [Header("Info")]
    [SerializeField] public VCType type;
    [SerializeField] public Vector3 cameraPos = Vector3.zero;
    [SerializeField] public Transform follow;
    [SerializeField] public Transform lookAt;

    [Header("카메라 종류")]
    [SerializeField] public CinemachineType cinemachineType = CinemachineType.Virtual;
    [SerializeField] public CinemachineVirtualInfo _virtualCam;
    [SerializeField] public CinemachineFreeLookInfo _freeLookCam;
}

#region 시네머신 - Virtual Camera
/// <summary>
/// 시네머신 가상 카메라 정보
/// </summary>
[System.Serializable]
public class CinemachineVirtualInfo
{
    [Header("Lens")]
    [SerializeField][Range(1, 179)] public float fieldOfView = 90f;
    [SerializeField] public OverrideModes lensMode = OverrideModes.None;

    [Header("Body")]
    [SerializeField] public VCBodyType bodyType;
    [SerializeField] public Body3PersonFollow bodyThridPersonFollow;
    [SerializeField] public BodyTransposer bodyTransposer;

    [Header("Aim")]
    [SerializeField] public VCAimType aimType;
    [SerializeField] public AimComposer aimComposer;
}

/// <summary>
/// 3인칭 추적 카메라 설정
/// </summary>
[System.Serializable]
public class Body3PersonFollow
{
    [Header("Rig")]
    [SerializeField] public Vector3 dampingValue;
    [SerializeField] public Vector3 shoulderOffset = Vector3.zero;
    [SerializeField][Range(0, 1)] public float cameraSide = 0.5f;
    [SerializeField] public float cameraDistance = 10f;

    [Header("Obstacles")]
    [SerializeField] public LayerMask cameraCollisionFilter;
    [SerializeField] public string ignoreTag;
}

/// <summary>
/// transposer 카메라 설정
/// </summary>
[System.Serializable]
public class BodyTransposer
{
    [SerializeField] public CinemachineTransposer.BindingMode bindingMode;
    [SerializeField] public Vector3 followOffset = new(0f, 0f, -10f);
    [SerializeField][Range(0, 20)] public float xDaming;
    [SerializeField][Range(0, 20)] public float yDaming;
    [SerializeField][Range(0, 20)] public float zDaming;
    [SerializeField][Range(0, 20)] public float yawDaming;
}

[System.Serializable]
public class AimComposer
{
    [SerializeField] public Vector3 trackedObjectOffset;
    [SerializeField][Range(-0.5f, 1.5f)] public float screenX = 0.5f;
    [SerializeField][Range(-0.5f, 1.5f)] public float screenY = 0.5f;
    [SerializeField][Range(0f, 2f)] public float deadZoneWidth = 0.5f;
    [SerializeField][Range(0f, 2f)] public float deadZoneHeight = 0.5f;
    [SerializeField][Range(0f, 2f)] public float softZoneWidth = 0.8f;
    [SerializeField][Range(0f, 2f)] public float softZoneHeight = 0.8f;
    [SerializeField][Range(-0.5f, 0.5f)] public float biasX;
    [SerializeField][Range(-0.5f, 0.5f)] public float biasY;
    [SerializeField] public bool centerOnActivate = true;
}
#endregion

#region 시네머신 - FreeLook Camera
[System.Serializable]
public class CinemachineFreeLookInfo
{

}
#endregion