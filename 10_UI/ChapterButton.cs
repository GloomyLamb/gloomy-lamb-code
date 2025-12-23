using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        int clear = dm.Current.ClearChapterNumber;

        if (chapter == clear) // 
        {
            Debug.Log($" Chapter {chapter} 성공! )");
        }
        else
        {
            Debug.Log($" Chapter {chapter} 실패! )");
        }
    }
}
