using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using UnityEngine.Networking;
//using Scripts;
using Proyecto26;
using UnityEditor;

public class SaveScript : MonoBehaviour　{

    public InputField inputField;
    public Text text;
    public static string str;
    string url = "https://silent-scanner-294501.an.r.appspot.com/";

    void Start()　{

        //InputFieldコンポーネントを取得
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
    }

    public void SaveText()　{

        str = inputField.textComponent.text;
        Debug.Log(str);

        //StartCoroutine(Post());
        RestClient.Post<Message>(url, new InputText { text = str }).Then(Message => {
            EditorUtility.DisplayDialog("JSON", JsonUtility.ToJson(Message, true), "Ok");
        });

        inputField.text = "";
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