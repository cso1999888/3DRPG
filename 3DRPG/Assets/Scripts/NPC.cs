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

    private AudioSource aud;

    private Player player;

    public int count;

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
        player.stop = true;

        Missioning();

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
    }

    //對話後將未接受任務改為任務進行中
    private void NoMission()
    {
        //如果狀態為未接受任務，將其改為進行任務中
        if (data._NPC_State == NPC_State.NoMission) data._NPC_State = NPC_State.Missioning;
    }

    //任務進行中切換為任務完成，需在對話開始時執行
    private void Missioning()
    {
        //如果數量 >= NPC 要求數量，將狀態改為完成
        if (count >= data.count) data._NPC_State = NPC_State.Finish;
    }

    private void Awake()
    {
        aud = GetComponent<AudioSource>();

        player = FindObjectOfType<Player>();                  //透過類型尋找物件  *僅限於場景上只有一個類型

        if (data._NPC_State != NPC_State.NoMission) data._NPC_State = NPC_State.NoMission;
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
