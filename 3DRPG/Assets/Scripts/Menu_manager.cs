using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu_manager : MonoBehaviour
{
    [Header("載入畫面")]
    public GameObject Panel_Loading;
    [Header("進度")]
    public Text Text_Loading;
    [Header("進度條")]
    public Image Image_Loading;
    [Header("要載入的遊戲畫面")]
    public string nameScene = "Game";
    [Header("提示文字")]
    public GameObject PressAnykey;

    //離開遊戲
    public void Quit() { Application.Quit(); }

    //開始遊戲
    public void StartGame()
    {
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        Panel_Loading.SetActive(true);                                     //顯示載入畫面
        AsyncOperation ao = SceneManager.LoadSceneAsync(nameScene);        //非同步載入場景（場景名稱）
        ao.allowSceneActivation = false;                                   //不要自動載入

        //當場景尚未載入完成
        while (!ao.isDone)
        {
            //Progress會從0跑到0.9，故/0.9讓他跑到1
            //Progress有小數點，加上ToString("F數字")，可以讓她跑到小數點後數字位，如ToString("F2")會跑到小數點後第二位
            Text_Loading.text = (ao.progress / 0.9f * 100).ToString("F0") + "%";            //更新文字
            Image_Loading.fillAmount = ao.progress / 0.9f;                                  //更新進度條
            yield return null;                                                              //等待一個影格

            if (ao.progress == 0.9f)
            {
                PressAnykey.SetActive(true);                                               //顯示提示文字

                if (Input.anyKeyDown) ao.allowSceneActivation = true;                      //按下任意鍵
            }
        }
    }
    
}
