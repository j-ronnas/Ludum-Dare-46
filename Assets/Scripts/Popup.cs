using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Popup : MonoBehaviour
{
    Text textComp;

    float timer;
    float showTime = 2f;

    Color baseColor;
    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<Text>();
        baseColor = textComp.color;
        timer = showTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>showTime)
        {
            return;
        }

        timer += Time.deltaTime;
        textComp.color = Color.Lerp(baseColor, Color.clear, timer / showTime);

    }

    public void ShowMessage(string message)
    {
        textComp.text = message;
        timer = 0;

    }
}
