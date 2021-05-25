using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : ScrollViewControl_Vertical<MessageGroupContent, MessageData>
{
    [SerializeField] private Scrollbar scrollbar;
    void Awake()
    {
        Init();
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
        ArrangementList();
    }


    public override void ArrangementList()
    {
        scrollbar.value = 0;
    }
}
