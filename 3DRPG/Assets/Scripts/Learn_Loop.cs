using UnityEngine;

public class Learn_Loop : MonoBehaviour
{
    int i = 0;
    public Transform cube;

    private void Start()
    {
        //if 布林值 = true，執行一次
        if (true)
        {
            print("判斷式");
        }
        //if 布林值 = true，持續執行
        while (i < 5)
        {
            i++;
            print("while 迴圈" + i + "號");
        }
        // for迴圈會幫你輸入所有資訊
        for (int i = 1; i <= 5; i++)
        {
            print("for 迴圈" + i + "號");
        }
        for (int j = 0; j < 20; j++)
        {
            Vector3 pos = new Vector3(j, 0, 0);
            //生成（物件，座標，角度）
            //Quaternion.identity → 角度 = 零
            Instantiate(cube, pos, Quaternion.identity);
        }
    }
}
