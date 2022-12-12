using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHP : MonoBehaviour
{
    public GameObject player;
    private string message;

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 60, 20), "");
        GUI.Label(new Rect(20, 10, 60, 20), message);
    }

    void Update()
    {
        if (player != null)
            message = "HP  " + player.GetComponent<Player>().GetHP().ToString();
        else
            message = "HP  0";
    }
}
