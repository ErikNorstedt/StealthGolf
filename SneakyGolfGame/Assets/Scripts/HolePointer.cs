using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HolePointer : MonoBehaviour
{
    private Vector3 targetPos;
    public RectTransform pointerRectTransform;
    private Transform player;
    private Image img;

    [SerializeField]
    private Camera uiCamera;

    float borderSize = 100;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPos = FindObjectOfType<Hole>().transform.position;
        img = pointerRectTransform.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        
        if (isOffScreen())
        {
            CapArrowPos();
            Rotate();
        }
        else
        {
            onScreenPos();
        }
    }

    bool isOffScreen()
    {

        Vector3 targetPosScreenPoint = Camera.main.WorldToScreenPoint(targetPos);
        bool tmp = targetPosScreenPoint.x <= borderSize || targetPosScreenPoint.x >= Screen.width - borderSize || targetPosScreenPoint.y <= borderSize || targetPosScreenPoint.y >= Screen.height - borderSize;
        
        return tmp;
    }

    void CapArrowPos()
    {
        Vector3 cappedTargetScreenPos = Camera.main.WorldToScreenPoint(targetPos);
        if (cappedTargetScreenPos.x <= borderSize) 
            cappedTargetScreenPos.x = borderSize;
        if (cappedTargetScreenPos.x >= Screen.width - borderSize) 
            cappedTargetScreenPos.x = Screen.width - borderSize;
        if (cappedTargetScreenPos.y <= borderSize)
            cappedTargetScreenPos.y = borderSize;
        if (cappedTargetScreenPos.y >= Screen.height - borderSize)
            cappedTargetScreenPos.y = Screen.height - borderSize;

        Vector3 pointerWorldPos = uiCamera.ScreenToWorldPoint(cappedTargetScreenPos);
        pointerRectTransform.position = pointerWorldPos;
        pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

        
    }
    void Rotate()
    {
        Vector3 toPos = targetPos;
        Vector3 fromPos = Camera.main.transform.position;
        fromPos.y = 0;
        Vector3 dir = (targetPos - fromPos).normalized;
        float angle = (Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg) % 360;
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
    void onScreenPos()
    {
        
        Vector3 pointerWorldPos = uiCamera.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(targetPos));
        pointerRectTransform.position = pointerWorldPos;
        pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, 180);
    }
}
