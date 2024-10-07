using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Source _source;

    private void Start()
    {
        _source = GameObject.Find("Source").GetComponent<Source>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * _source.PlayerCameraSensivityX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * _source.PlayerCameraSensivityY;

        _source.PlayerCameraRotationX -= mouseY;

        _source.PlayerCameraRotationX = Mathf.Clamp(_source.PlayerCameraRotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_source.PlayerCameraRotationX, 0f, 0f);

        _source.PlayerObject.transform.Rotate(Vector3.up * mouseX);
    }
}
