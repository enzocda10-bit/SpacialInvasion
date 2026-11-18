using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    private Image imageComponent;
    private bool isButtonHighlighted = false;
    [SerializeField] private float highlightSpeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        imageComponent = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isButtonHighlighted)
        {
            imageComponent.fillAmount = Mathf.Lerp(imageComponent.fillAmount, 1f, Time.deltaTime * highlightSpeed);
        }
        else
        {
            imageComponent.fillAmount = Mathf.Lerp(imageComponent.fillAmount, 0f, Time.deltaTime * highlightSpeed);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isButtonHighlighted = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isButtonHighlighted = true;
    }
}
