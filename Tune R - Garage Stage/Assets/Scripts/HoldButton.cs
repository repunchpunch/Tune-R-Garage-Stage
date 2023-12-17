using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private float pointerDownTimer;

    [SerializeField]
    private float requiredHoldTime;

    public Button workBtn;

    void Awake()
    {
        workBtn = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= requiredHoldTime)
            {
                pointerDownTimer = 0;
                OnHoldComplete();
            }
        }
    }

    private void OnHoldComplete()
    {
        Inventory.Singleton.SpawnItemForWork();
    }

    private void Reset()
    {
        pointerDown = false; 
        pointerDownTimer = 0;
    }
}