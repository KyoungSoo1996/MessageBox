
using System;

public class MessageData
{
    public int checker;
    public string message;
    public string name;
    public DateTime time;

    public MessageData SetData(int _checker, string _message, string _name, DateTime _time)
    {
        MessageData temp = new MessageData();
        temp.checker = _checker;
        temp.message = _message;
        temp.name = _name;
        temp.time = _time;
        return temp;
    }
}
