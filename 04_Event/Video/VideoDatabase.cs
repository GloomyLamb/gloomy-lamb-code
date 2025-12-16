using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "new VideoDatabase", menuName = "SO/VideoDatabase")]
public class VideoDatabase : ScriptableObject
{
    public List<VideoClipEntry> clips;
}

[System.Serializable]
public class VideoClipEntry
{
    public VideoID id;
    public VideoClip videoClip;
}
