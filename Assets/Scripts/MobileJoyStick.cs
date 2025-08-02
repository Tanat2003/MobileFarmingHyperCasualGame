using UnityEngine;


public class MobileJoyStick : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickKnob;
    [Header("Setting")]
    [SerializeField] private float moveFactor;
    private Vector3 clickedPosition;
    private Vector3 move;
    private bool canControlJoyStick;
    private Vector3 clickPosition;
    private void Start()
    {
        HideJoyStick();
    }
    private void Update()
    {
        if (canControlJoyStick)
        {
            ControlJoyStick();
        }
    }
    public void CallJoyStickZone()
    {
        Vector2 inputPosition;

#if UNITY_EDITOR
        inputPosition = Input.mousePosition;
#else
    if (Input.touchCount > 0)
    {
        inputPosition = Input.GetTouch(0).position;
    }
    else
    {
        return; 
    }
#endif

        clickPosition = inputPosition;
        joystickOutline.position = clickPosition;
        ShowJoyStick();
        canControlJoyStick = true;
    }

    //public void CallJoyStickZone()
    //{


    //    clickPosition = Input.mousePosition;
    //    joystickOutline.position = clickPosition;
    //    ShowJoyStick();
    //    canControlJoyStick = true;
    //}
    #region Hide&ShowJoyStick
    private void ShowJoyStick()
    {
        joystickOutline.gameObject.SetActive(true);
    }
    private void HideJoyStick()
    {
        joystickOutline.gameObject.SetActive(false);
        canControlJoyStick = false;
        move = Vector3.zero;
    }
    #endregion
    private void ControlJoyStick()
    {

        Vector3 currentPosition;//= Input.mousePosition;

#if UNITY_EDITOR
        currentPosition = Input.mousePosition;
#else
    if (Input.touchCount > 0)
    {
        currentPosition = Input.GetTouch(0).position;
    }
    else
    {
        return; 
    }
#endif




        Vector3 direction = currentPosition - clickPosition;
        //joyStickKnob �ǧOutline
        float moveMagnitude = direction.magnitude * moveFactor / Screen.width; //moveFactor��͵���äӹǹ���з��JoyStickKnob������͹���
        //�ӡѴ���С�â�ѺJoystickKnob�������ԹOutline
        moveMagnitude = Mathf.Min(moveMagnitude, joystickOutline.rect.width / 2);

        //���˹觷���Ѻ��JoyStick
        move = direction.normalized * moveMagnitude;

        //���˹� �¤ӹǹ�ҡ���˹觷�衴ŧ��˹�Ҩ� + ���µ��˹觷���Ѻ��JoyStick
        Vector3 targetPosition = clickPosition + move;

        joystickKnob.position = targetPosition;
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            HideJoyStick();
        }
#else
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
    {
        HideJoyStick();
    }
#endif
        if (Input.GetMouseButtonUp(0))
            HideJoyStick();
    }
    public Vector3 GetMouseVector() => move;


}
