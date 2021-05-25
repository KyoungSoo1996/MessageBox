using UnityEngine;
using System;
using System.Net;
using System.Globalization;
using System.Collections;

public class TimeUtils : MonoBehaviour
{
    public static TimeUtils Inst = null;
    private void Awake()
    {
        if (Inst == null) Inst = this;
    }

    int timeCount = 0;
    DateTime currentTime = default(DateTime);


    public DateTime GetDateTime()
    {
        //리턴 할 날짜 선언
        // Google
        DateTime dateTime = DateTime.MinValue;
        try
        {
            using (var response = WebRequest.Create("http://www.google.com").GetResponse())
                dateTime = DateTime.ParseExact(response.Headers["date"],
                    "yyyy-MM-dd HH:mm:ss tt 'UTC'",
                    CultureInfo.InvariantCulture.DateTimeFormat,
                    DateTimeStyles.AssumeUniversal);
        }
        // Local
        catch (Exception)
        {
            dateTime = DateTime.UtcNow;
        }
        return dateTime;
    }

    ///<summary>
    ///시간을 설정할 수 있는 함수 인자값 : 시간, 분, 초, AM/PM
    ///</summary>
    public DateTime GetMakeTime(int _hour, int _min, int _sec, bool _sun)
    {
        return Convert.ToDateTime(_hour.ToString() + ":" + _min.ToString() + ":" + _sec.ToString() + " " + (_sun ? "AM" : "PM"));
    }

    // Current Time to UTC Current num(현재시간)
    public int GetCurrentTime()
    {
        TimeSpan time = (GetDateTime() - new DateTime(2020, 1, 1));
        return (int)time.TotalSeconds;
    }

    // input : addTime / output : currentTime + addtime(OO초뒤 시간)
    public int GetAfterTime(int _addTime)
    {
        TimeSpan time = (GetDateTime() - new DateTime(2020, 1, 1).AddSeconds(_addTime));
        return (int)time.TotalSeconds;
    }

    public TimeSpan GetAfterTimeSpan(int _addTime)
    {
        TimeSpan time = (GetDateTime() - new DateTime(2020, 1, 1).AddSeconds(_addTime));
        return time;
    }

    // 현재 시간을 string으로 출력하기
    public DateTime GetCurrentTimetoStr()
    {
        DateTime resultTime;
        if (currentTime == default(DateTime))
        {
            currentTime = GetDateTime().AddHours(9); // 한국시간으로 변경
            //curTime = currentTime.Year.ToString("D4") + currentTime.Month.ToString("D2") + currentTime.Day.ToString("D2") + currentTime.Hour.ToString("D2") + currentTime.Minute.ToString("D2") + currentTime.Second.ToString("D2");
            StartCoroutine(Timer());
            resultTime = currentTime;
        }
        else
        {
            resultTime = currentTime.AddSeconds(timeCount);
            //curTime = currentTime.Hour >= 12 ? $"PM {(currentTime.Hour % 12).ToString("D2")} : {currentTime.Minute.ToString("D2")}" : $"AM {(currentTime.Hour % 12).ToString("D2")} : {currentTime.Minute.ToString("D2")}";
        }
        return resultTime;
    }
    WaitForSeconds second = new WaitForSeconds(1);
    private IEnumerator Timer()
    {
        timeCount += 1;
        yield return second;
        StartCoroutine(Timer());
    }
}