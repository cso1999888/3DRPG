using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0.1f, 3)]
    public float speed = 2.5f;
    [Header("攻擊力"), Range(35f, 50f)]
    public float attack = 40f;
    [Header("血量"), Range(200, 300)]
    public float HP = 200;
    [Header("經驗值"), Range(30, 100)]
    public float exp = 30;
    [Header("攻擊距離"), Range(0.1f, 3)]
    public float distanceAttack = 1.5f;
    [Header("攻擊冷卻"), Range(0.1f, 5)]
    public float cd = 2.5f;
    [Header("轉向速度"), Range(0.1f, 50)]
    public float turn = 5f;


    private NavMeshAgent nav;
    private Animator ani;
    private Transform player;
    private float timer;                //計時器


    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        nav.speed = speed;
        nav.stoppingDistance = distanceAttack;              //設定攻擊停止距離

        player = GameObject.Find("Player").transform;
        nav.SetDestination(player.position);                //避免一開始偷打
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other);

        if(other.name == "Player")
        {
            other.GetComponent<Player>().Hit(attack);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.35f);
        Gizmos.DrawSphere(transform.position, distanceAttack);
    }

    private void Move()
    {
        nav.SetDestination(player.position);                        //追蹤玩家座標
        ani.SetFloat("Move", nav.velocity.magnitude);               //設定移動動畫

        if (nav.remainingDistance <= distanceAttack) Attack();
        //如果 距離 <= 攻擊停止距離 → 攻擊
    }


    /// <summary>
    /// 攻擊：動畫
    /// </summary>
    private void Attack()
    {
        Quaternion look = Quaternion.LookRotation(player.position - transform.position);                //面向向量  看向角度(玩家座標 - 自己座標)
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * turn);         //角度 = 插植(角度，面向角度，速度)

        timer += Time.deltaTime;       //計時器累加

        if(timer >= cd)
        {
            timer = 0;
            ani.SetTrigger("Attack");
        }

    }
}
