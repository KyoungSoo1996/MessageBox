using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SheetIndexData
{
    public string URL;
    public int Index;
}

public class ChatData
{
    public int time;
    public string name;
    public string message;
    public ChatData SetData(int _time, string _name, string _message)
    {
        ChatData result = new ChatData();
        result.time = _time;
        result.name = _name;
        result.message = _message;
        return result;
    }
}

public class SheetChatDatas
{
    public string URL;

}

public class GoogleSheetManager : MonoBehaviour
{
    private string chatStartIndexURL = "https://docs.google.com/spreadsheets/d/1AnI6_cYhFI6w40gW4UqtIBE7GWhAvpRsP4QGGB2CeR8/export?gid=0&format=tsv&range=D2:D2";
    private string chatEndIndexURL = "https://docs.google.com/spreadsheets/d/1AnI6_cYhFI6w40gW4UqtIBE7GWhAvpRsP4QGGB2CeR8/export?gid=0&format=tsv&range=E2:E2";
    private string ChattingURL = "https://script.google.com/macros/s/AKfycbyiPF0CgaPpi7aAWJP0LdWjSSMsuiwqmov3IH5wIgwr3Jv-32sxDF-Fu1gS0ezJnIC3/exec";

    string startIndex ="0", endIndex ="0";

    private void Start()
    {
        StartCoroutine(GetGoogleStartIndexData());
        StartCoroutine(GetGoogleEndIndexData());
        StartCoroutine(GetGoogleChatData());
        ChatPost("hello", "world!");
    }

    private IEnumerator GetGoogleChatData()
    {
        while (true)
        {
            Debug.Log("wait");
            if (startIndex != "0" && endIndex != "0")
            {
                Debug.Log(startIndex);
                Debug.Log(endIndex);
                WWWForm form = new WWWForm();
                using (UnityWebRequest www = UnityWebRequest.Get($"https://docs.google.com/spreadsheets/d/1AnI6_cYhFI6w40gW4UqtIBE7GWhAvpRsP4QGGB2CeR8/export?gid=0&format=tsv&range=A{startIndex}:C{endIndex}"))
                {
                    yield return www.SendWebRequest();
                    if (www.isDone)
                    {
                        Debug.Log(www.downloadHandler.text);
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
                startIndex = www.downloadHandler.text;
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
                endIndex = www.downloadHandler.text;
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
    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(ChattingURL, form))
        {
            yield return www.SendWebRequest();
        }
    }
}
