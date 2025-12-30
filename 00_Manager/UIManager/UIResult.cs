using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIResult : MonoBehaviour
{
    [SerializeField] private GameObject clearObj;
    [SerializeField] private GameObject failedObj;
    private float delayTime = 5f;
    [SerializeField] private string NextSceneName = "LibraryScene";
    private bool _shown;
    private Coroutine _routine;
    private void Awake()
    {
        if (clearObj) clearObj.SetActive(false);
        if (failedObj) failedObj.SetActive(false);
    }

    public void ShowClear()
    {
        if (_shown) return;
        _shown = true;

        if (clearObj) clearObj.SetActive(true);
        if (failedObj) failedObj.SetActive(false);
        
        if(_routine != null) StopCoroutine(_routine);
        _routine = StartCoroutine(AfterDelay(NextSceneName));
    }

    public void ShowFailed()
    {
        if (_shown) return;
        _shown = true;

        if (clearObj) clearObj.SetActive(false);
        if (failedObj) failedObj.SetActive(true);
      
    }
    private IEnumerator AfterDelay(string LibraryScene)
    {
        yield return new WaitForSeconds(delayTime);
        Time.timeScale = 1f;
        SceneManager.LoadScene(LibraryScene);
    }
}
