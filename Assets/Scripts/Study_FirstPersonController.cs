using System;
using UnityEngine;

public class Study_FirstPersonController : MonoBehaviour
{
    // WASD로 좌표이동
    // L-Shift로 달리기
    // 마우스로 회전 
    
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    
    [Header("Mouse Settings")]
    [SerializeField] private float horizontalSensitivity = 1.0f;
    [SerializeField] private float verticalSensitivity = 1.0f;
    
    // 과제 힌트 (카메라만 위아래 움직이게 하면 될듯? )
    [SerializeField] private Transform cameraTransform;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        UpdatePosition();
        UpdateRotation();
    }

    // 이동은 자기 local 기준 좌표계로 이동 
    private void UpdatePosition()
    {
        // 사용자의 입력 받기
        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        // 햔재 transform의 올바른 좌표계로 바꿔주기

        Vector3 forward = transform.forward * inputAxis.y;
        Vector3 right = transform.right * inputAxis.x;
        
        // 위의 forward와 right를 합치면 ? 
        // 입력에 따라 캐릭터가 이동할 방향백터가 나옵니다.
        // 이제부터 moveVector 라고 부릅니다. (백터 + 백터 = 방향백터)
        Vector3 moveVector = (forward + right).normalized;

        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    private void UpdateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity;
        
        // Rotate 함수는 특정 축(Axis)으로 Euler를 더해줌
        // (나중에는 Quaternion 단위로 사용함. 사실은 더해주는것이아님)
        transform.Rotate(Vector3.up * mouseX);
        
        
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;
        cameraTransform.Rotate(Vector3.right * -mouseY);
        
        // 각도 제한 어떻게 두지 
    }
}
