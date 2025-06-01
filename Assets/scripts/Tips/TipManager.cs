using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TipManager : MonoBehaviour
{
    public static Action<string> displayTipEvent;
    public static Action disableTipEvent;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private GameObject tipPanel;
    private Animator anim;
    private int activeTips;
    

    private void OnEnable()
    {
        displayTipEvent += DisplayTip;
        disableTipEvent += DisableTip;
    }

    private void OnDisable()
    {
        displayTipEvent -= DisplayTip;
        disableTipEvent -= DisableTip;
    }
    
    private void DisplayTip(string message)
    {
        messageText.text = message;
        tipPanel.SetActive(true);
    }

    private void DisableTip()
    {
        tipPanel.SetActive(false);
    }
}
