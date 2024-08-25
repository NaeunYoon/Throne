using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class Joystick : MonoBehaviour,IBeginDragHandler, IDragHandler,IEndDragHandler
{
    [SerializeField]
    private PlayerController _playerManager;

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
        _playerManager._player_Animator.SetInteger("ACTION", 0);
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
