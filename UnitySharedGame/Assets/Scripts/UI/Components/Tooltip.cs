using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string message = null;
    private bool isShowing;

    public void OnMouseEnter()
    {
        if(message != null && !isShowing)
        {
            ShowToolTip();
        }
    }

    public void OnMouseOver()
    {
        if(message == null && isShowing)
        {
            HideTooltip();
        }
        else if(message != null && !isShowing)
        {
            ShowToolTip();
        }
    }

    public void OnMouseExit()
    {
        if (isShowing)
        {
            HideTooltip();
        }
    }

    public void ShowToolTip()
    {
        isShowing = true;
        TooltipManager.instance.SetAndShowTooltip(message);
    }

    public void HideTooltip()
    {
        isShowing = false;
        TooltipManager.instance.HideTooltip();
    }
}
