using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位
    public float speed = 0.1f;
    public float turn = 5;
    private float attack = 10;
    private float HP = 100;
    private float MP = 50;
    private float EXP;
    private int Lv = 1;

    //在屬性面板上隱藏
    [HideInInspector]
    //停止：讓玩家不能移動
    public bool stop;

    [Header("傳送門：0 NPC；1 ENEMY")]
    public Transform[] teleports;

    private Rigidbody rig;
    private Animator ani;
    private Transform cam;   //攝影機根物件

    public AudioSource aud;
    public AudioClip soundProp;

    public NPC npc;
    #endregion

    #region 事件
    private void Awake()
    {
        //取得元件<泛型>();
        //泛型：所有類型
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        cam = GameObject.Find("Camera_RootObject").transform;

        npc = FindObjectOfType<NPC>();
    }
    private void FixedUpdate()
    {
        if (stop) return;              //如果停止→跳出（無法移動）
        Move();
    }
    private void OnCollisionEnter(Collision collision)   //Is Trigger = false
    {
        if (collision.gameObject.tag == "Skull") GetProp(collision.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Teleport")
        {
            transform.position = teleports[1].position;                          //如果碰到NPC端→傳送至Enemy端
            teleports[1].GetComponent<CapsuleCollider>().enabled = false;        //關閉Enemy端碰撞
            Invoke("OpenTeleportEnemy", 3);
        }
        if (other.name == "Teleport_EnemySide") 
        {
            transform.position = teleports[0].position;                          //如果碰到Enemy端→傳送至端
            teleports[0].GetComponent<CapsuleCollider>().enabled = false;        //關閉NPC端碰撞
            Invoke("OpenTeleportNPC", 3);
        }
    }
    #endregion

    #region 方法
    private void OpenTeleportNPC()
    {
        teleports[0].GetComponent<CapsuleCollider>().enabled = true;
    }
    private void OpenTeleportEnemy()
    {
        teleports[1].GetComponent<CapsuleCollider>().enabled = true;
    }
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
    private void GetProp(GameObject prop)
    {
        Destroy(prop);
        aud.PlayOneShot(soundProp);
        npc.UpdateTextMission();
    }
    public void Hit(float damage)
    {
        HP -= damage;
        ani.SetTrigger("Hit");
    }
    private void Dead()
    {

    }
    private void Exp()
    {

    }
    #endregion
}
