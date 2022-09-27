using Lunity.AudioVis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class KochLine : KochGenerator
{
    LineRenderer _lineRenderer;
   //[Range(0,1)]
   //public float _lerpAmount;
    public float _generateMultiplier;
    private float[] _lerpAudio;
    Vector3[] _lerpPosition;

    [Header("Audio")]
    public SoundCapture _soundCapture;
    public AudioAverageSet _audioAverageSet;
    public Material _material;
    public Color _color;
    private Material _matInstance;
    public int _audioBandMaterial;
    public float _emissionMultiplier;
    //public int _audioBand;
    // Start is called before the first frame update
    void Start()
    {
        _lerpAudio = new float[_initiatorPointAmount];
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.positionCount = _position.Length;
        _lineRenderer.SetPositions(_position);
        _lineRenderer.positionCount = _position.Length;
        _lerpPosition = new Vector3[_position.Length];
        //apply material
        _matInstance = new Material(_material);
        _lineRenderer.material = _matInstance;
        _matInstance.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        _matInstance.SetColor("_EmissionColor", _color * _soundCapture.BarData[_audioBandMaterial] * _emissionMultiplier);

        if (_generationCount != 0)
        {
            int count = 0;
            for (int i = 0; i < _initiatorPointAmount; i++)
            {
                _lerpAudio[i] = _soundCapture.BarData[i];
                for (int j = 0; j <(_position.Length - 1) / _initiatorPointAmount; j++)
                {
                    _lerpPosition[count] = Vector3.Lerp(_position[count], _targetPosition[count], _lerpAudio[i]);
                    count++;
                }

            }
            _lerpPosition[count] = Vector3.Lerp(_position[count], _targetPosition[count], _lerpAudio[_initiatorPointAmount - 1]);

            //for (int i = 0; i < _position.Length; i++)
            //{
            //_lerpPosition[i] = Vector3.Lerp(_position[i], _targetPosition[i], _soundCapture.PeakVolume);
            //_lerpPosition[i] = Vector3.Lerp(_position[i], _targetPosition[i], _audioAverageSet.Momentary);
            //}
            if (_useBezierCurves)
            {
                _bezierPosition = BezierCurve(_lerpPosition, _bezierVertexCount);
                _lineRenderer.positionCount = _bezierPosition.Length;
                _lineRenderer.SetPositions(_bezierPosition);
            }
            else
            {
                _lineRenderer.positionCount = _lerpPosition.Length;
                _lineRenderer.SetPositions(_lerpPosition);
            }
            
        }
    }
}
