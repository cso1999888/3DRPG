using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位
    public float speed = 0.1f;
    public float turn = 5;
    private int attack = 10;
    private int HP = 100;
    private int MP = 50;
    private int EXP;
    private int Lv = 1;

    private Rigidbody rig;
    private Animator ani;
    private Transform cam;   //攝影機根物件
    #endregion

    #region 事件
    private void Awake()
    {
        //取得元件<泛型>();
        //泛型：所有類型
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        cam = GameObject.Find("Camera_RootObject").transform;

    }
    private void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region 方法
    private void Move()
    {
        float v = -Input.GetAxis("Vertical");                                                 //前後 = WS & 上下
        float h = -Input.GetAxis("Horizontal");                                               //左右 = AD & 左右
        Vector3 pos = cam.forward * v + cam.right * h;                                       //移動座標 = 攝影機.前方 * 前後 + 攝影機.右方 * 左右
        rig.MovePosition(transform.position + pos * speed);                                  //移動座標（原本座標 + 移動座標 * 速度）

        ani.SetFloat("Move", Mathf.Abs(v) + Mathf.Abs(h));                                   //設定浮點數(絕對值 V 與 H)


        if(v != 0 || h != 0)                                                                //如果 控制中
        {
            pos.y = 0;              //移動座標 y 不動
            Quaternion angle = Quaternion.LookRotation(pos);                                //B 角度 = 面向（移動座標）
            transform.rotation = Quaternion.Slerp(transform.rotation, angle, turn);         //A 角度 = 角度.插值（A 角度，B 角度，旋轉角度）
        }
    }
    private void Attack()
    {

    }
    private void Skill()
    {

    }
    private void GetProp()
    {

    }
    private void Hit()
    {

    }
    private void Dead()
    {

    }
    private void Exp()
    {

    }
    #endregion
}
