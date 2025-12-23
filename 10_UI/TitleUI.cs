using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : BaseUI
{
    [SerializeField] Button startButton;

    protected override void Init()
    {
        startButton?.onClick.AddListener(OnStartGame);
    }   
    public void OnStartGame()
    {
        GameManager.Instance.ShowVideo(VideoID.Test, SceneType.LibraryScene);
    }

}
