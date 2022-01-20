using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class FollowMouseUI : MonoBehaviour
{
    public event EventHandler OnZoomIn;
    public event EventHandler OnZoomOut;
     
    [SerializeField] private Animator animator;
    [SerializeField] private CinemachineVirtualCamera playerCamera;

    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out Vector2 localPoint);
        GetComponent<RectTransform>().localPosition = localPoint;

        if (Input.GetMouseButtonDown(1))
        {
            //animator.SetBool("ScopeUp", true);
            Cursor.visible = false;
            playerCamera.m_Lens.FieldOfView = 50;
        }

        if (Input.GetMouseButtonUp(1))
        {
            //animator.SetBool("ScopeUp", false);
            Cursor.visible = true;
            playerCamera.m_Lens.FieldOfView= 80;
        }
    }


}
