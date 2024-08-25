using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class VirtualJoystick : UIBase,IBeginDragHandler, IDragHandler,IEndDragHandler
{
    [SerializeField]
    private PlayerManager _playerManager;

    [SerializeField]
    private RectTransform lever;

    [SerializeField]
    private RectTransform joystick;


    [SerializeField, Range(1,150)]
    private float leverRange;

    private Vector2 inputDirection;
    public Vector2 INPUT_DIRECTION
    {
        set { inputDirection = value; }
        get { return inputDirection; }
    }

    public bool isInput;

    public override void Create()
    {
        //joystick = GetComponent<RectTransform>();
    }

    public void GetPlayer(PlayerManager player)
    {
        _playerManager = player;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControllJoystickLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControllJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        _playerManager.Move(Vector2.zero);
        _playerManager.anim.SetInteger("anim", 0);
    }

    private void ControllJoystickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - joystick.anchoredPosition;

        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;  
    }

    public void InputControlVector()
    {
        _playerManager.Move(inputDirection);
    }


    void Update()
    {
        if(isInput == true)
        {
            InputControlVector();
        }
    }
}
