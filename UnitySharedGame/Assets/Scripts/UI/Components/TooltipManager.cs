using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;
    public Canvas MyCanvas;
    public TextMeshProUGUI textComponent;

    private RectTransform rectTransform;
    private RectTransform canvasTransform;
    private const int offset = 10;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasTransform = MyCanvas.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var pos = Input.mousePosition;

        float pivotX = Mathf.Round(pos.x / Screen.width);
        float pivotY = Mathf.Round(pos.y / Screen.height);

        if( pivotX == 0)
        {
            pos += Vector3.right * offset;
        }
        else
        {
            pos += Vector3.left * offset;
        }
        if(pivotY != 0)
        {
            pos += Vector3.down * offset;
        }

        Vector3 output = Vector2.zero;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvasTransform,
            pos,
            Camera.main,
            out output);

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = output;
    }

    public void SetAndShowTooltip(string text)
    {
        gameObject.SetActive(true);
        textComponent.text = text;
        var layout = GetComponent<LayoutElement>();
        layout.preferredWidth = textComponent.preferredWidth + 4f;
        layout.preferredHeight = textComponent.preferredHeight + 4f;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }
}
