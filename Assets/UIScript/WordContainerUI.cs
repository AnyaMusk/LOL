using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class WordContainerUI : MonoBehaviour, IDropHandler
{
    private WordUI prevWordUI;
    private WordUI currentWordUI;
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            currentWordUI = eventData.pointerDrag.GetComponent<WordUI>();
            currentWordUI.isPlaced = true;
            //eventData.pointerDrag.GetComponent<RectTransform>().position =

            eventData.pointerDrag.GetComponent<RectTransform>().DOMove(GetComponent<RectTransform>().position, 0.35f);
            
            if (prevWordUI == null) prevWordUI = currentWordUI;
            if (prevWordUI != currentWordUI)
            {
                prevWordUI.isPlaced = false;
                prevWordUI.ResetPosition();
                prevWordUI = currentWordUI;
            }

        }
    }
}
