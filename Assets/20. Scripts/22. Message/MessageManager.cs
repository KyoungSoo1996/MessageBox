using System.Collections.Generic;
using UnityEngine;

public class MessageManager : ScrollViewControl_Vertical<MessageGroupContent, MessageData>
{
    void Awake()
    {
        Init();
        Debug.Log(TimeUtils.Inst.GetCurrentTimetoStr());
    }

    List<MessageData> MessageDatas = new List<MessageData>();
    MessageData temp = new MessageData();
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            if (i % 3 == 0)
            {
                MessageDatas.Add(temp.SetData(i % 3, i.ToString(), null, TimeUtils.Inst.GetCurrentTimetoStr()));
            }
            else
            {
                MessageDatas.Add(temp.SetData(i % 3, "hello World " + i.ToString(), i.ToString(), TimeUtils.Inst.GetCurrentTimetoStr()));
            }
        }
        SetData(MessageDatas);
    }
}
