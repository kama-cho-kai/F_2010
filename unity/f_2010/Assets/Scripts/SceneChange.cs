using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン切り替えに使用するライブラリ

public class SceneChange : MonoBehaviour {

    public void changeScence() {

        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

    public void SaveName() {

        string namae = "A";
        string token = "ad4ebea43ff4e2e94e94ac28ce3d57c07c8bd668";

        PlayerPrefs.SetString("NAME", namae);
        PlayerPrefs.SetString("TOKEN", token);
        PlayerPrefs.Save();

        Debug.Log(namae);
    }
}