using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Source _source;

    void Start()
    {
        _source = GameObject.Find("Source").GetComponent<Source>();
    }

    private void Update()
    {
        transform.position = _source.PlayerCameraPosition.position;
    }
}
