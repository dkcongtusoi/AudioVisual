using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
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
    private Vector2 _tsL;
    [Range(0,1)]
    public float _distOuterTangent;
    // Start is called before the first frame update
    void Start()
    {
        _innerCircleGO = (GameObject)Instantiate(_circlePrefab);
        _outterCircleGO = (GameObject)Instantiate(_circlePrefab);
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
        _innerCircle = new Vector4(
            _tsL.x * (_outterCircle.w - _innerCircle.w) + _outterCircle.x,
            0.0f,
            _tsL.y * (_outterCircle.w - _innerCircle.w) + _outterCircle.z,
            _innerCircle.w);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        _innerCircleGO.transform.position = new Vector3(_innerCircle.x, _innerCircle.y, _innerCircle.z);
        _innerCircleGO.transform.localScale = new Vector3(_innerCircle.w, _innerCircle.w, _innerCircle.w) * 2;
        _outterCircleGO.transform.position = new Vector3(_outterCircle.x, _outterCircle.y, _outterCircle.z);
        _outterCircleGO.transform.localScale = new Vector3(_outterCircle.w, _outterCircle.w, _outterCircle.w) * 2;

        for (int i = 0; i < _circleAmount; i++)
        {
            _tangentCircle[i] = FindTangentCircle(_outterCircle, _innerCircle, (360f / _circleAmount) * i);
            _tangentObject[i].transform.position = new Vector3(_tangentCircle[i].x, _tangentCircle[i].y, _tangentCircle[i].z);
            _tangentObject[i].transform.localScale = new Vector3(_tangentCircle[i].w, _tangentCircle[i].w, _tangentCircle[i].w) * 2;
        }

    }
}
