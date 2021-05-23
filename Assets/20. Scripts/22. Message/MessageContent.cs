using UnityEngine;
using TMPro;
using System;

public class MessageContent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextMessage, TextName, TextTime;
    public void SetData(string _message, string _name, DateTime _time)
    {
        TextMessage.text = _message;
        TextName.text = _name;
        TextTime.text = _time.Hour >= 12 ? $"PM {(_time.Hour % 12).ToString("D2")} : {_time.Minute.ToString("D2")}" : $"AM {(_time.Hour % 12).ToString("D2")} : {_time.Minute.ToString("D2")}";;
    }

    public void SetData(string _message)
    {
        TextMessage.text = _message;
    }
}
