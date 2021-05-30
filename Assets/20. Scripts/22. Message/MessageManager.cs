using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : ScrollViewControl_Vertical<MessageGroupContent, MessageData>
{
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private InputField inputField;

    void Awake()
    {
        Init();
    }

    private void Start()
    {
        GoogleSheetManager.Inst.GoogleSheetManagerStart(() => { ArrangementList(); });
    }

    List<MessageData> MessageDatas = new List<MessageData>();
    MessageData temp = new MessageData();
    public string myNickName = "";

    public void SetChats(List<ChatData> _chatdatas)
    {
        foreach (ChatData _data in _chatdatas)
        {
            MessageDatas.Add(temp.SetData(myNickName == _data.name ? 2 : 1, _data.message, _data.name, _data.time));
        }
        SetData(MessageDatas);
    }

    public void UpdateChats(List<ChatData> _chatDatas)
    {
        foreach (ChatData _data in _chatDatas)
        {
            MessageDatas.Add(temp.SetData(myNickName == _data.name ? 2 : 1, _data.message, _data.name, _data.time));
        }
        UpdateData(MessageDatas);
        ArrangementList();
    }

    public override void ArrangementList()
    {

    }

    public void OnClickSendMessage()
    {
        GoogleSheetManager.Inst.ChatPost(myNickName, inputField.text);
        inputField.text = "";
    }
}
