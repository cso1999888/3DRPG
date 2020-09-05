using UnityEngine;

//列舉 = 下拉式選單
public enum NPC_State
{
    NoMission, Missioning, Finish
}

//ScriptableObject 腳本化物件：可儲存於專案的資料
[CreateAssetMenu(fileName = "NPC 資料", menuName = "NPC/資料")]
public class NPC_Data : ScriptableObject
{
    [Header("NPC狀態")]
    public NPC_State _NPC_State = NPC_State.NoMission;
    [Header("任務需求數量")]
    public int count = 20;
    [Header("對話：未取得任務、任務進行中、任務完成")]
    public string[] dialogues = new string[3];

}
 