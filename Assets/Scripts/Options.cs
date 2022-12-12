using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private bool isMute = false;
    private bool isWindowed = false;

    //public Toggle mute;
    //public Toggle fullscreen;
    //public Toggle windowed;

    private Toggle mute;
    private Toggle fullscreen;
    private Toggle windowed;

    public GameObject mainPanel;

    private void Start()
    {
        mute = GameObject.Find("Mute").GetComponent<Toggle>();
        isMute = AudioListener.volume == 0;
        mute.isOn = isMute;
        fullscreen = GameObject.Find("Fullscreen").GetComponent<Toggle>();
        windowed = GameObject.Find("Windowed").GetComponent<Toggle>();
        isWindowed = Screen.fullScreen;
        fullscreen.isOn = !isWindowed;
        windowed.isOn = isWindowed;
    }

    public void ChangeSetting()
    {
        isMute = mute.isOn;
        isWindowed = windowed.isOn;
    }

    public void Apply()
    {
        AudioListener.volume = isMute ? 0 : 1;
        Screen.fullScreen = isWindowed;
        mainPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        mainPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
