using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.PlaySfxOnce(SfxName.Test);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundManager.Instance.PlayBgm(BgmName.TestBgm);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundManager.Instance.StopBgm();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);            
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundManager.Instance.SetMasterVolume(SoundManager.Instance.MasterVolume + 0.1f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SoundManager.Instance.SetMasterVolume(SoundManager.Instance.MasterVolume - 0.1f);
        }
    }
}
