using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterButton : MonoBehaviour
{
    private DataManager dm;

    private void Awake()
    {
        dm = new DataManager();
        dm.Load();

        Debug.Log($"[로드 완료] ClearChapterNumber = {dm.Current.ClearChapterNumber}");
    }

    // 버튼 OnClick에 연결할 함수들
    public void OnClickChapter1() => Check(1);
    public void OnClickChapter2() => Check(2);
    public void OnClickChapter3() => Check(3);

    private void Check(int chapter)
    {
        dm.Load();
        int clear = dm.Current.ClearChapterNumber;
        int playableChapter = clear + 1;

        if (chapter == playableChapter) // 
        {
            Debug.Log($" Chapter {chapter} 성공! )");

            LoadChapterScene(chapter);
        }
        else
        {
            Debug.Log($" Chapter {chapter} 실패! )");
        }
    }

    private void LoadChapterScene(int chapter)
    {
        switch (chapter)
        {
            case 1:
                SceneManager.LoadScene("ShadowForestScene");
                break;

          //  case 2:
                SceneManager.LoadScene("Chapter2Scene"); // 나중에 채울때 추가하면 됨
                break;

           // case 3:
                SceneManager.LoadScene("Chapter3Scene");
                break;
        }
    }

}
