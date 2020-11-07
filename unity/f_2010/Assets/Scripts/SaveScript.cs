using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using UnityEngine.Networking;
//using Scripts;
using Proyecto26;
using UnityEditor;

public class SaveScript : MonoBehaviour　{

    public static InputField inputField;
    public Text text;
    public static string str;
    static string url = "https://silent-scanner-294501.an.r.appspot.com/get_message";
    public static string m;

    void Start()　{

        //InputFieldコンポーネントを取得
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
    }

    public void SaveText()　{

        str = inputField.textComponent.text;
        Debug.Log(str);
        /*
        string namae = PlayerPrefs.GetString("NAME", "");
        string token = PlayerPrefs.GetString("TOKEN", "");
        */
        string namae = "たくと";
        string token = "ad4ebea43ff4e2e94e94ac28ce3d57c07c8bd668";
        //StartCoroutine(Post());
        RestClient.Post<Message>(url, new InputText { text = str, name = namae, token = token }).Then(message => {
            //string response = JsonUtility.ToJson(message, true);
            //EditorUtility.DisplayDialog("JSON", message.message, "Ok");
            Debug.Log(message.message);
            m = message.message;
            Debug.Log(m);
            text.GetComponent<Text>().text = m;
            //Fukidashi.fukidashi(m);
        });
        Debug.Log(m);
        //m = "hello";
        inputField.text = "";
    }

    public static string getMessage() {

        return m;
    }

    public void DisplayText() {

        text.GetComponent<Text>().text = str;
    }

    private void Post() {
        /*
		var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        */

		//yield return request.SendWebRequest();

		//Message message = new Message();
        /*
        if (request.isNetworkError) {

            Debug.Log(request.error);

        } else {

            //リクエストが成功した時
            if (request.responseCode == 200) {

                string jsonText = request.downloadHandler.text;
                jsonText = System.Text.RegularExpressions.Regex.Unescape(jsonText);
                Debug.Log(jsonText);

                hoge = JsonUtility.FromJson<HogeData>(jsonText);
            }
        }*/
    }
}