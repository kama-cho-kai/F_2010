using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fukidashi : MonoBehaviour　{

    public Text text;

    public void fukidashi() {

        text.GetComponent<Text>().text = SaveScript.getMessage();
    }
}
