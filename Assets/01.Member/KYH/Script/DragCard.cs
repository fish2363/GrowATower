using UnityEngine;
using UnityEngine.EventSystems;

public class DragCard : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{
    [field: SerializeField] public Test myInfo { get; private set; }
    private GridSystem gridSystem;

    public void Initialze(Test info,GridSystem grid)
    {
        myInfo = info;
        gridSystem = grid;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        gridSystem.SetGrid(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gridSystem.SetGrid(false);
    }
}
