using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class TangentCircles : CircleTangent
{
    public GameObject _circlePrefab;
    private GameObject _innerCircleGO, _outterCircleGO;
    public Vector4 _innerCircle, _outterCircle;
    private Vector4[] _tangentCircle;
    private GameObject[] _tangentObject;
    [Range(1,64)]
    public int _circleAmount;

    //INPUT
    private Vector2 _tsL, _tsLSmooth;
    [Range(0,1)]
    public float _distOuterTangent;
    [Range(0, 1)]
    public float _movementSmooth;
    private float _radiusChange;
    [Range(0.1f, 10f)]
    public float _radiusChangeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //_innerCircleGO = (GameObject)Instantiate(_circlePrefab);
        //_outterCircleGO = (GameObject)Instantiate(_circlePrefab);
        _tangentCircle = new Vector4[_circleAmount];
        _tangentObject = new GameObject[_circleAmount];
        for (int i = 0; i < _circleAmount; i++)
        {
            GameObject tangentInstance = (GameObject)Instantiate(_circlePrefab);
            _tangentObject[i] = tangentInstance;
            _tangentObject[i].transform.parent = this.transform;
        }
    }

    void PlayerInput()
    {
        _tsL = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _tsLSmooth = new Vector2(
            _tsLSmooth.x * (1 - _movementSmooth) + _tsL.x * _movementSmooth,
            _tsLSmooth.y * (1 - _movementSmooth) + _tsL.y * _movementSmooth);

        _radiusChange = Input.GetAxis("TriggerL") - Input.GetAxis("TriggerR");


        _innerCircle = new Vector4(
            (_tsLSmooth.x * (_outterCircle.w - _innerCircle.w) * (1 - _distOuterTangent)) + _outterCircle.x,
            0.0f,
            (_tsLSmooth.y * (_outterCircle.w - _innerCircle.w) * (1 - _distOuterTangent)) + _outterCircle.z,
            _innerCircle.w + (_radiusChange * Time.deltaTime * _radiusChangeSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
       // _innerCircleGO.transform.position = new Vector3(_innerCircle.x, _innerCircle.y, _innerCircle.z);
       // _innerCircleGO.transform.localScale = new Vector3(_innerCircle.w, _innerCircle.w, _innerCircle.w) * 2;
       // _outterCircleGO.transform.position = new Vector3(_outterCircle.x, _outterCircle.y, _outterCircle.z);
       // _outterCircleGO.transform.localScale = new Vector3(_outterCircle.w, _outterCircle.w, _outterCircle.w) * 2;

        for (int i = 0; i < _circleAmount; i++)
        {
            _tangentCircle[i] = FindTangentCircle(_outterCircle, _innerCircle, (360f / _circleAmount) * i);
            _tangentObject[i].transform.position = new Vector3(_tangentCircle[i].x, _tangentCircle[i].y, _tangentCircle[i].z);
            _tangentObject[i].transform.localScale = new Vector3(_tangentCircle[i].w, _tangentCircle[i].w, _tangentCircle[i].w) * 2;
        }

    }
}
