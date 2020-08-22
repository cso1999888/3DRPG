using UnityEngine;
using System.Collections; //使用協程需要此API

public class Learn_Coroutine : MonoBehaviour
{
    public Transform player;


    private void Start()
    {
        //啟動協程
        StartCoroutine(Test());
        StartCoroutine(Enlarge());
    }
    //定義協程
    //傳回類型必須是 IEnumerator 傳回時間
    public IEnumerator Test()
    {
        print("協程");
        yield return new WaitForSeconds(5);
        print("5秒後的協程");
    }
    public IEnumerator Enlarge()
    {
        for (int i = 0; i < 10; i++)
        {
            player.localScale += Vector3.one;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
