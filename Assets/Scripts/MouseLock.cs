using UnityEngine;

public class MouseLock : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private float _mouseSensivity = 100f;

    private float _mouseX;
    private float _mouseY;
    private float xRotation = 0f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        _mouseX = Input.GetAxis("Mouse X") * _mouseSensivity * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _mouseSensivity * Time.deltaTime;
        xRotation -= _mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        _player.Rotate(Vector3.up * _mouseX);
    }
}
