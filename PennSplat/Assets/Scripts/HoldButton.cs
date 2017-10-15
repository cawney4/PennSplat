using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoldButton : Button
{
    private bool buttonHeldDown;
    private PointerEventData _data;

    void Update()
    {
        if (buttonHeldDown)
        {
            OnPointerClick(_data);
        }
    }


    public override void OnPointerDown(PointerEventData data)
    {
        buttonHeldDown = true;
        _data = data;
    }

    public override void OnPointerUp(PointerEventData data)
    {
        buttonHeldDown = false;
        _data = data;
    }
}
