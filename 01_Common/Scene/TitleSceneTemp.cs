using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneTemp : MonoBehaviour
{
    // todo: TitleUI 생기면 옮겨야해요
    [SerializeField] Button startButton;

    private void Awake()
    {
        startButton.onClick.AddListener(OnStartGame);
    }

    public void OnStartGame()
    {
        GameManager.Instance.ShowVideo(VideoID.Test, SceneType.LibraryScene);
    }
}
