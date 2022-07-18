using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class ScreenCharacteristics
{
    private float _width;
    private float _height;

    public float Width
    {
        get { return _width; }
        set { if(value>=0) _width = value; }
    }

    public float Height
    {
        get { return _height; }
        set { if (value >= 0) _height = value; }
    }

    public float AspectRatio
    {
        get {
            if (Height == 0) return 0;
            return Height / Width; 
        }
    }

    public ScreenCharacteristics(float width = 0, float height = 0)
    {
        Width = width;
        Height = height;
    }
}

[RequireComponent(typeof(Camera))]
public class MovementCamera2D : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects;
    [SerializeField] private List<MovementCameraTrigger> _movementTriggers;
    [SerializeField] private Vector2 _minDistanceBetweenObjectsToNeedMove;
    [SerializeField] private float _minCameraSize;
    [SerializeField] private float _changeTimeDuration;
    [SerializeField] private float _intervalCameraChangeTime;
    [SerializeField] private float _screenAcpectWidth;
    [SerializeField] private float _screenAcpectHeight;


    private Camera _camera;
    private ScreenCharacteristics _screen;

    private Vector2 _maxDistadistantBetweenObjects;

    private Vector2 _currentAddtitionalSize;
    private Vector2 _ñurrentCenterPosition;
    private Vector2 _previousAddtitionalSize;
    private Vector2 _previousCenterPosition;

    private bool _isCameraChanging;

    private void Awake()
    {
        _isCameraChanging = false;

        _camera = GetComponent<Camera>();
        _camera.orthographicSize = _minCameraSize;
        _screen = new ScreenCharacteristics(_screenAcpectWidth, _screenAcpectHeight);

        _previousAddtitionalSize = new Vector2();
        _previousCenterPosition = new Vector2();

        foreach (var trigger in _movementTriggers)
        {
            trigger.MovementCameraTriggered += StartChangeCharacteristics;
        }

        StartCoroutine(IntervalCameraChange(_intervalCameraChangeTime));
    }
    
    private IEnumerator IntervalCameraChange(float intervalCameraChangeTime)
    {
        var wait = new WaitForSeconds(intervalCameraChangeTime);
        while (true)
        {
            yield return wait;
            StartChangeCharacteristics();
        }
    }
    private void StartChangeCharacteristics()
    {
        if (!_isCameraChanging)
        {
            _isCameraChanging=true;
            StartCoroutine(ChangeCameraCharacteristics(_changeTimeDuration));
        }
    }

    private IEnumerator ChangeCameraCharacteristics(float changeTimeDuration)
    {
        float changeTime = 0;
        DeleteNullableObjects(_objects); 
        SetDistanseAndCenterPoint(_objects);

        float xSize = _maxDistadistantBetweenObjects.x - _minDistanceBetweenObjectsToNeedMove.x;
        xSize = xSize > 0 ? xSize : 0;
        float ySize = _maxDistadistantBetweenObjects.y - _minDistanceBetweenObjectsToNeedMove.y;
        ySize = ySize > 0 ? ySize : 0;
        _currentAddtitionalSize = new Vector2(xSize, ySize);
        while (changeTime < changeTimeDuration)
        {
            float coefficient = changeTime / changeTimeDuration;
            MoveCamera(_previousCenterPosition, _ñurrentCenterPosition, coefficient);
            ResizeCamera(_previousAddtitionalSize, _currentAddtitionalSize, coefficient);
            changeTime += Time.deltaTime;
            yield return null;
        }
        _previousCenterPosition = _ñurrentCenterPosition;
        _previousAddtitionalSize = _currentAddtitionalSize;
        _isCameraChanging = false;
    }

    private void DeleteNullableObjects(List<GameObject> _objects)
    {
        List<GameObject> needToRemove = new List<GameObject>();
        foreach (var obj in _objects)
            if (obj == null)
                needToRemove.Add(obj);

        foreach (var obj in needToRemove)
            _objects.Remove(obj);
    }
    private void SetDistanseAndCenterPoint(IEnumerable<GameObject> _objects)
    {
        Vector2 max = new Vector2(-1, -1);
        Vector2 distanse = new Vector2();
        foreach (var obj1 in _objects)
        {
            foreach (var obj2 in _objects)
            {
                distanse.x = Mathf.Abs(obj1.transform.position.x - obj2.transform.position.x);
                if (distanse.x > max.x)
                {
                    _ñurrentCenterPosition.x = (obj1.transform.position.x + obj2.transform.position.x) / 2f;
                    max.x = distanse.x;
                }
                distanse.y = Mathf.Abs(obj1.transform.position.y - obj2.transform.position.y);
                if (distanse.y > max.y)
                {
                    _ñurrentCenterPosition.y = (obj1.transform.position.y + obj2.transform.position.y) / 2f;
                    max.y = distanse.y;
                }
            }
        }
        _maxDistadistantBetweenObjects = max;
    }
    
    private void MoveCamera(Vector2 previousAddtitionalSize, Vector2 currentCenterPosition, float coefficient)
    {
        Vector2 center = Vector2.Lerp(previousAddtitionalSize, currentCenterPosition, coefficient);
        _camera.transform.position = new(center.x, center.y, _camera.transform.position.z);
    }

    private void ResizeCamera(Vector2 previusCenterPosition, Vector2 currentAddtitionalSize, float coefficient)
    {
        Vector2 additionalSize = Vector2.Lerp(previusCenterPosition, currentAddtitionalSize, coefficient);
        float addSize = additionalSize.x * (_screen.AspectRatio/2.5f) > additionalSize.y * (_screen.AspectRatio / 2.5f) 
            ? additionalSize.x * (_screen.AspectRatio / 2.5f) 
            : additionalSize.y * (_screen.AspectRatio / 2.5f);
        //Need to found formule depends screenAspectRatio...
        _camera.orthographicSize = _minCameraSize + addSize;
        _camera.transform.localScale = new Vector3(1f + addSize / _minCameraSize, 1f + addSize / _minCameraSize, 1);
    }

}
