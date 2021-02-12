using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler
{

    [SerializeField] protected RectTransform background = null;
    [SerializeField] private RectTransform handle = null;

    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera cam;

    private Vector2 input = Vector2.zero;

    int a = 0;

    [SerializeField] private float handleRange = 1;
    [SerializeField] private float deadZone = 0;

    protected virtual void Start()
    {

        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
            Debug.LogError("The Joystick is not placed inside a canvas");

    }
    public void OnDrag(PointerEventData eventData)
    {
        cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            cam = canvas.worldCamera;

        //백그라운드 이미지의 위치
        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);

        //핸들의 위치를 제한
        Vector2 radius = background.sizeDelta / 2;

        Debug.Log(position);
        Debug.Log(eventData.position);
        Debug.Log((eventData.position - position));


        input = (eventData.position - position) / (radius * canvas.scaleFactor);

    }

    protected virtual void HandleInput(float magnitude, Vector2 normalised)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
                input = normalised;
        }
        //else
        //    input = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            cam = canvas.worldCamera;

        //백그라운드 이미지의 위치
        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);

        //핸들의 위치를 제한
        Vector2 radius = background.sizeDelta / 2;

        //핸들의 위치를 연산
        input = (eventData.position - position) / radius; //(* canvas.scaleFactor);

        Debug.Log(input.normalized);
        Debug.Log(input);
        //핸들의 위치를 지정
        handle.anchoredPosition = input * radius;// * handleRange;
    }
}
