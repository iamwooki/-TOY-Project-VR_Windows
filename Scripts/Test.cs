using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Test : MonoBehaviour {
    GameObject x;

    public string weather = "";
    int cnt = 0;

    // Use this for initialization
    void Start()
    {
        XmlDocument docX = new XmlDocument(); // XmlDocument 생성

        try
        {
            docX.Load("http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=2729058500"); // url로 xml 파일 로드
        }
        catch
        {
            return;
        }

        XmlNodeList hourList = docX.GetElementsByTagName("hour"); // 태그 이름으로 노드 리스트 저장
        XmlNodeList tempList = docX.GetElementsByTagName("temp");
        XmlNodeList weatherList = docX.GetElementsByTagName("wfKor");

        // 활용 예제
        weather = "   = 대구 날씨 =\n";
        for (int i = 0; i < hourList.Count; i++)
        {
            if (hourList[i].InnerText == "6")
            {
                weather += "아침 : " + weatherList[i].InnerText + " (" + tempList[i].InnerText + "℃)\n";
                cnt++;
            }
            if (hourList[i].InnerText == "12")
            {
                weather += "점심 : " + weatherList[i].InnerText + " (" + tempList[i].InnerText + "℃)\n";
                cnt++;
            }
            if (hourList[i].InnerText == "18")
            {
                weather += "저녁 : " + weatherList[i].InnerText + " (" + tempList[i].InnerText + "℃)\n";
                cnt++;
            }
            if (cnt == 3) break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
