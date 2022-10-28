using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private Vector2 _tsR, _tsRSmooth;
    public int _rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }
    void PlayerInput()
    {
        _tsR = new Vector2(Input.GetAxis("HorizontalR"), Input.GetAxis("VerticalR"));
        this.transform.Rotate(_tsR.x * _rotateSpeed * Time.deltaTime, _tsR.y * _rotateSpeed * Time.deltaTime, 0);

        if (Input.GetButton("LeftBumper"))
        {
            _rotateSpeed++;
        }
        if (Input.GetButton("RightBumper"))
        {
            _rotateSpeed--;
        }
        _rotateSpeed = Mathf.Clamp(_rotateSpeed, 10, 400);
    }
}
