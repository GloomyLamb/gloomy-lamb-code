using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "new VideoDatabase", menuName = "SO/VideoDatabase")]
public class VideoDatabase : ScriptableObject
{
    public List<VideoClipEntry> clips;

    private Dictionary<VideoID, VideoClip> _cache;

    private void OnEnable()
    {
        _cache = new();
        foreach (var clip in clips)
        {
            if (!_cache.ContainsKey(clip.id))
            {
                _cache.Add(clip.id, clip.videoClip);
            }
        }
    }

    public bool TryGetClip(VideoID id, out VideoClip clip)
    {
        return _cache.TryGetValue(id, out clip);
    }
}

[System.Serializable]
public class VideoClipEntry
{
    public VideoID id;
    public VideoClip videoClip;
}
