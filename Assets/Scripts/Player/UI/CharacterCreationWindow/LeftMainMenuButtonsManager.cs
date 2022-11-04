using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftMainMenuButtonsManager : MonoBehaviour
{
    public void DisableAllChildButtonInteraction()
    {
        Button[] childButtons = GetComponentsInChildren<Button>();
        foreach(Button childButton in childButtons)
        {
            childButton.interactable = false;
        }
    }

    public void EnableAllChildButtonInteraction()
    {
        Button[] childButtons = GetComponentsInChildren<Button>();
        foreach (Button childButton in childButtons)
        {
            childButton.interactable = true;
        }
    }
}
