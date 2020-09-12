using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("NPC 資料")]
    public NPC_Data data;
    [Header("對話區塊")]
    public GameObject Panel_Dialogue;
    [Header("任務區塊")]
    public GameObject Panel_Quest;
    [Header("名稱")]
    public Text textName;
    [Header("內容")]
    public Text textContent;
    [Header("打字速度"), Range(0.1f, 1)]
    public float printSpeed = 0.05f;
    [Header("打字音效")]
    public AudioClip soundPrint;
    [Header("任務區塊")]
    public RectTransform panelMission; //RectTransform 為區塊專用 
    [Header("任務數量")]
    public Text textMission;

    private AudioSource aud;
    private Animator ani;
    private Player player;

    public int count;

    //更新任務介面
    public void UpdateTextMission()
    {
        count++;
        textMission.text = count + " / " + data.count;
    }

    //對話系統
    public void Dialogue()
    {
        Panel_Dialogue.SetActive(true);
        Panel_Quest.SetActive(true);
        textName.text = name;
        StartCoroutine(Print());
    }

    //打字效果
    private IEnumerator Print()
    {
        AnimationControll();

        Missioning();

        player.stop = true;

        string dialogue = data.dialogues[(int)data._NPC_State];                             //NPC對話第一段
        textContent.text = "";                                                              //清空

        for (int i = 0; i < data.dialogues[(int)data._NPC_State].Length; i++)               //跑對話的第一個字到最後一個字
        {
            textContent.text += dialogue[i];                                                //對話文字.文字 += 對話[]
            aud.PlayOneShot(soundPrint, 0.5f);
            yield return new WaitForSeconds(printSpeed);
        }

        player.stop = false;

        NoMission();
    }

    //取消對話
    private void CancelDialogue()
    {
        Panel_Dialogue.SetActive(false);
        ani.SetBool("Talk", false);
    }

    /// <summary>
    /// 對話後將未接受任務改為任務進行中
    /// </summary>
    private void NoMission()
    {
        //如果狀態為未接受任務，將其改為進行任務中
        if (data._NPC_State == NPC_State.NoMission)
        {
            data._NPC_State = NPC_State.Missioning;
            StartCoroutine(ShowMission());
        }    
    }

    private IEnumerator ShowMission()
    {
        // 當任務區塊的x大於480就用插植跑到480
        while(panelMission.anchoredPosition.x > 15)
        {
            panelMission.anchoredPosition = Vector3.Lerp(panelMission.anchoredPosition, new Vector3(15, 76, 0), 10 * Time.deltaTime);
            yield return null;
        }
    }

    /// <summary>
    /// 任務進行中切換為任務完成，需在對話開始時執行
    /// </summary>
    private void Missioning()
    {
        //如果數量 >= NPC 要求數量，將狀態改為完成
        if (count >= data.count) data._NPC_State = NPC_State.Finish;
    }

    /// <summary>
    /// 動畫控制
    /// </summary>
    private void AnimationControll()
    {
        if (data._NPC_State == NPC_State.NoMission | data._NPC_State == NPC_State.Missioning)
            ani.SetBool("Talk", true);
        else
            ani.SetTrigger("Thank");
    }

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();

        player = FindObjectOfType<Player>();                  //透過類型尋找物件  *僅限於場景上只有一個類型

        data._NPC_State = NPC_State.NoMission;                //遊戲開始時將NPC狀態更動為NO Mission
    }

    //進入
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") Dialogue();
    }

    //離開
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player") CancelDialogue();
    }
}
