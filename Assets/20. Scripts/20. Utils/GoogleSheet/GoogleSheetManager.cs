using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChatData
{
    public string time;
    public string name;
    public string message;
    public ChatData SetData(string _time, string _name, string _message)
    {
        ChatData result = new ChatData();
        result.time = _time;
        result.name = _name;
        result.message = _message;
        return result;
    }
}

public class GoogleSheetManager : MonoSingleton<GoogleSheetManager>
{
    public List<ChatData> ServerChatDatas = new List<ChatData>();
    public MessageManager messageManager;


    private string chatStartIndexURL = "https://docs.google.com/spreadsheets/d/1AnI6_cYhFI6w40gW4UqtIBE7GWhAvpRsP4QGGB2CeR8/export?gid=0&format=tsv&range=D2:D2";
    private string chatEndIndexURL = "https://docs.google.com/spreadsheets/d/1AnI6_cYhFI6w40gW4UqtIBE7GWhAvpRsP4QGGB2CeR8/export?gid=0&format=tsv&range=E2:E2";
    private string ChattingsURL = "https://script.google.com/macros/s/AKfycbyiPF0CgaPpi7aAWJP0LdWjSSMsuiwqmov3IH5wIgwr3Jv-32sxDF-Fu1gS0ezJnIC3/exec";

    int startIndex = 0, endIndex = 0;


    public void GoogleSheetManagerStart(Action _action)
    {
        StartCoroutine(GetGoogleStartIndexData());
        StartCoroutine(GetGoogleEndIndexData());
        StartCoroutine(GetGoogleChatDatas(() => { StartCoroutine(GetGoogleChatData(() => { _action?.Invoke(); })); }));
    }

    private IEnumerator GetGoogleChatData(Action _action)
    {
        while (true)
        {
            WWWForm form = new WWWForm();
            using (UnityWebRequest www = UnityWebRequest.Get(chatEndIndexURL))
            {
                yield return www.SendWebRequest();
                if (www.isDone)
                {
                    endIndex = int.Parse(www.downloadHandler.text);
                    if (startIndex != endIndex)
                    {
                        using (UnityWebRequest wwww = UnityWebRequest.Get($"https://docs.google.com/spreadsheets/d/1AnI6_cYhFI6w40gW4UqtIBE7GWhAvpRsP4QGGB2CeR8/export?gid=0&format=tsv&range=A{startIndex.ToString()}:C{endIndex.ToString()}"))
                        {
                            yield return wwww.SendWebRequest();
                            if (wwww.isDone)
                            {
                                messageManager.UpdateChats(TSVReader.Inst.Read(wwww.downloadHandler.text));
                                startIndex = endIndex;
                                _action?.Invoke();
                            }
                        }
                    }
                }
            }
            yield return null;
        }
    }

    private IEnumerator GetGoogleChatDatas(Action _action)
    {
        while (true)
        {
            if (startIndex.ToString() != "0" && endIndex.ToString() != "0")
            {
                WWWForm form = new WWWForm();
                using (UnityWebRequest www = UnityWebRequest.Get($"https://docs.google.com/spreadsheets/d/1AnI6_cYhFI6w40gW4UqtIBE7GWhAvpRsP4QGGB2CeR8/export?gid=0&format=tsv&range=A{startIndex.ToString()}:C{endIndex.ToString()}"))
                {
                    yield return www.SendWebRequest();
                    if (www.isDone)
                    {
                        ServerChatDatas = TSVReader.Inst.Read(www.downloadHandler.text);
                        messageManager.SetChats(ServerChatDatas);
                        startIndex = endIndex;
                        _action?.Invoke();
                    }
                }
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator GetGoogleStartIndexData()
    {
        WWWForm form = new WWWForm();
        using (UnityWebRequest www = UnityWebRequest.Get(chatStartIndexURL))
        {
            yield return www.SendWebRequest();
            if (www.isDone)
            {
                startIndex = int.Parse(www.downloadHandler.text);
            }
        }
    }

    private IEnumerator GetGoogleEndIndexData()
    {
        WWWForm form = new WWWForm();
        using (UnityWebRequest www = UnityWebRequest.Get(chatEndIndexURL))
        {
            yield return www.SendWebRequest();
            if (www.isDone)
            {
                endIndex = int.Parse(www.downloadHandler.text);
            }
        }
    }

    public void ChatPost(string _nickName, string _message)
    {
        WWWForm form = new WWWForm();
        form.AddField("time", TimeUtils.Inst.GetCurrentTimetoStr().ToString());
        form.AddField("nickname", _nickName);
        form.AddField("message", _message);
        StartCoroutine(Post(form));
    }

    private IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(ChattingsURL, form))
        {
            yield return www.SendWebRequest();
        }
    }
}
