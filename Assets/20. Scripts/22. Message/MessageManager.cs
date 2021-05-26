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
        ArrangementList();
    }


    public override void ArrangementList()
    {
        scrollbar.value = 0;
    }

    public void OnClickSendMessage()
    {
        Debug.Log(GoogleSheetManager.Inst == null);
        GoogleSheetManager.Inst.ChatPost(myNickName, inputField.text);
        inputField.text = "";
    }
}
