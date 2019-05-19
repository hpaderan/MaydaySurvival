using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Color NeutralColor;
    public Color HoverColor;

    public void ButtonEnter(Text ThisButton)
    {
        ThisButton.color = HoverColor;
    }

    public void ButtonExit(Text ThisButton)
    {
        ThisButton.color = NeutralColor;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ColorBlind(int TF)
    {
        PlayerPrefs.SetInt("CBlind", TF);
    }

    public void LargeText(int TF)
    {
        PlayerPrefs.SetInt("LText", TF);
    }
}
