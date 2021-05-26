using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MessageContent : MonoBehaviour
{
    [SerializeField] private Text TextMessage, TextName, TextTime;
    public void SetData(string _message, string _name, string _time)
    {
        TextMessage.text = _message;
        TextName.text = _name;
        DateTime date = Convert.ToDateTime(_time);
        TextTime.text = date.Hour >= 12 ? $"PM {(date.Hour % 12).ToString("D2")} : {date.Minute.ToString("D2")}" : $"AM {(date.Hour % 12).ToString("D2")} : {date.Minute.ToString("D2")}"; ;
    }

    public void SetData(string _message)
    {
        TextMessage.text = _message;
    }
}
