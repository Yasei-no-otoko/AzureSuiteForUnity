using UnityEngine;

[System.Serializable]
public class BingSpeechResponse
{
    public string version;

    public HeaderItem header;

    public ResultItem[] results;

    public static BingSpeechResponse CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<BingSpeechResponse>(jsonString);
    }

    // Given JSON input:
    // {"name":"Dr Charles","lives":3,"health":0.8}
    // this example will return a PlayerInfo object with
    // name == "Dr Charles", lives == 3, and health == 0.8f.

}

[System.Serializable]
public class HeaderItem
{
    public string status;
    public string scenario;
    public string name;
    public string lexical;
}

[System.Serializable]
public class ResultItem
{
    public string scenario;
    public string name;
    public string lexical;
    public float confidence;
}
/*
{
    "version":"3.0",
    "header":
    {
        "status":"success",
        "scenario":"ulm",
        "name":"テストテスト",
        "lexical":"テストテスト",
        "properties":
        {
            "requestid":"113e6622-ddf1-40a6-bd94-f05009764019",
            "LOWCONF":"1"
        }
    },
    "results":[
        {
            "scenario":"ulm",
            "name":"テストテスト",
            "lexical":"テストテスト",
            "confidence":"0.3455283",
            "properties":{"LOWCONF":"1"}
        }
    ]
}
*/