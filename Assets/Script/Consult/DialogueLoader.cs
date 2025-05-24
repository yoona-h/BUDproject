using UnityEngine;
using System.Collections.Generic;
using System.IO;


[System.Serializable]
public class TalkingData
{
    public int id;
    public string type;
    public string speaker;
    public string[] texts;
    public int[] nextIds;
    public StatChange[] statChanges;
    public string[] commands;
}
[System.Serializable]
public class StatChange
{
    public int index;
    public int value;
}


public class DialogueLoader : MonoBehaviour
{
    public TextAsset[] csvFiles;

    public Dictionary<int, TalkingData> dialogueDict = new Dictionary<int, TalkingData>();

    void Awake()
    {
        LoadCSV(0);// GameData.Instance.talk_branch);
    }

    void LoadCSV(int talk_branch)
    {
        if (csvFiles[talk_branch] == null)
        {
            Debug.LogError("❌ CSV 파일이 연결되지 않았습니다.");
            return;
        }

        string[] lines = csvFiles[talk_branch].text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] cols = lines[i].Split(',');

            int id = int.Parse(cols[0].Trim());
            string type = cols[1].Trim();
            string speaker = cols[2].Trim();
            string[] texts = cols[3].Trim().Split(';');
            string[] nextIdStrs = cols[4].Trim().Split(';');
            string[] statStrs = cols[5].Trim().Split(';');
            string[] commandStrs = cols.Length > 6 ? cols[6].Trim().Split(';') : new string[texts.Length];

            // nextIds
            int[] nextIds = new int[nextIdStrs.Length];
            for (int j = 0; j < nextIdStrs.Length; j++)
            {
                int.TryParse(nextIdStrs[j], out nextIds[j]);
            }

            // statChanges
            StatChange[] statChanges = new StatChange[statStrs.Length];
            for (int j = 0; j < statStrs.Length; j++)
            {
                if (string.IsNullOrWhiteSpace(statStrs[j])) continue;

                string stat = statStrs[j];
                char sign = stat.Contains('+') ? '+' : stat.Contains('-') ? '-' : ' ';
                if (sign == ' ') continue;

                var parts = stat.Split(sign);
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int index) &&
                    int.TryParse(parts[1], out int value))
                {

                    if (sign == '-') value *= -1;
                    statChanges[j] = new StatChange { index = index, value = value };
                }
            }

            // commands
            string[] commands = new string[commandStrs.Length];
            for (int j = 0; j < commandStrs.Length; j++)
            {
                commands[j] = commandStrs[j];
            }

            TalkingData data = new TalkingData
            {
                id = id,
                type = type,
                speaker = speaker,
                texts = texts,
                nextIds = nextIds,
                statChanges = statChanges,
                commands = commands
            };

            dialogueDict[id] = data;
        }
    }
}