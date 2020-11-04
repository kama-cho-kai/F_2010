using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour {

    //InputFieldを格納
    InputField inputField;
    public Text text;

    void Start() {

        //InputFieldコンポーネントを取得
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
    }

    //入力されたテキスト情報を読み取りコンソールに出力
    public void GetInputText() {

        //InputFieldからテキスト情報を取得
        string text = inputField.textComponent.text;
        Debug.Log(text);

        //入力フォームのテキストを空にする
        inputField.text = "";
    }

    public void DisplayText(){

        text.GetComponent<Text>().text = inputField.textComponent.text;
    }
}
