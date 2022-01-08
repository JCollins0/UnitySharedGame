using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public GameObject fillBar;
    public GameObject minLine;
    public GameObject maxLine;
    public int minValue;
    public int maxValue;
    public int currentValue;

    private RectTransform fillBarTransform;
    private RectTransform myTransform;

    private RectTransform minLineTransform;
    private RectTransform maxLineTransform;
    private bool hasProcessingPenality;

    public void LoadRecipe(CraftingRecipe recipe)
    {
        hasProcessingPenality = recipe.hasProcessingPenality;
        minLine.SetActive(hasProcessingPenality);
        maxLine.SetActive(hasProcessingPenality);
        if (hasProcessingPenality)
        {
            Debug.Log("Setting active" + recipe);
            minLineTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, ((float)recipe.minProcessingTime / maxValue * myTransform.rect.width), 1);
            maxLineTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, ((float)recipe.maxProcessingTime / maxValue * myTransform.rect.width), 1);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        fillBarTransform = fillBar.GetComponent<RectTransform>();
        myTransform = GetComponent<RectTransform>();

        minLineTransform = minLine.GetComponent<RectTransform>();
        maxLineTransform = maxLine.GetComponent<RectTransform>();

        minLine.SetActive(false);
        maxLine.SetActive(false);
    }

    void FixedUpdate()
    {
        fillBarTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((float)currentValue / maxValue) * myTransform.rect.width);
        currentValue = (currentValue + 1) % maxValue;
    }
}
