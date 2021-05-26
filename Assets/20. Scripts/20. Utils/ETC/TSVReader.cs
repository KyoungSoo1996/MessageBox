using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TSVReader : MonoSingleton<TSVReader>
{
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    public List<ChatData> Read(string data)
    {
        List<ChatData> result = new List<ChatData>();
        ChatData temp = new ChatData();
        foreach (string words in Regex.Split(data, LINE_SPLIT_RE))
        {
            string[] text = words.Split('\t');
            result.Add(temp.SetData(text[0], text[1], text[2]));
        }

        return result;
    }
}
