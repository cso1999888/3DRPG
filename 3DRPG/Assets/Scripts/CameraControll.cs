using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [Header("目標")]
    public Transform target;
    [Header("速度"), Range(0, 100)]
    public float speed = 1;
    [Header("旋轉速度"), Range(0, 100)]
    public float turn = 1;
    [Header("上下角度限制")]
    public Vector2 limit = new Vector2(-30, 30);


    // 滑鼠控制旋轉角度
    private Quaternion rot;

    //追蹤
    private void Track()
    {
        Vector3 PosA = transform.position;                                           // A 點
        Vector3 PosB = target.position;                                              // B 點
        PosA = Vector3.Lerp(PosA, PosB, Time.deltaTime * speed);                     // A 點 = Vector3.插值(A 點，B 點，Time.deltaTime * speed);
        transform.position = PosA;                                                   // 攝影機.座標 = A 點

        //旋轉
        rot.x += Input.GetAxis("Mouse Y") * turn;                                    //取得滑鼠上下來控制 X 角度
        rot.y += Input.GetAxis("Mouse X") * turn;                                    //取得滑鼠左右來控制 Y 角度

        rot.x = Mathf.Clamp(rot.x, limit.x, limit.y);                                //限制 x 在 -30 和 30 內

        transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);                  //攝影機根物件.角度 = 歐拉(x,y,z);
    }

    private void Awake()
    {
        Cursor.visible = false;             //隱藏鼠標
    }

    private void LateUpdate()
    {
        Track();
    }
}
