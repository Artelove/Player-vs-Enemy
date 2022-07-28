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
    [SerializeField] private List<GameObject> _observedObjects;
    [SerializeField] private Vector2 _minDistanceBetweenObjectsForCameraMove;
    [SerializeField] private float _changeEpsilon = 0.1f;
    [SerializeField] private float _minCameraSize = 7f;
    [SerializeField] private float _maxCameraSize = 10f;
    [SerializeField] private float _screenAcpectWidth = 16f;
    [SerializeField] private float _screenAcpectHeight = 9f;


    private Camera _camera;
    private ScreenCharacteristics _screen;

    private Vector2 _maxDistadistantBetweenObjects;
    private Vector2 _currentAddtitionalSize;
    private Vector2 _ñurrentCenterPosition;
    private Vector2 _previousAddtitionalSize;
    private Vector2 _previousCenterPosition;

    private void Awake()
    {

        _camera = GetComponent<Camera>();
        _camera.orthographicSize = _minCameraSize;
        _screen = new ScreenCharacteristics(_screenAcpectWidth, _screenAcpectHeight);

        _previousAddtitionalSize = new Vector2();
        _previousCenterPosition = new Vector2();
    }

    private void Update()
    {
        ChangeCameraCharacteristics();
    }
    private void ChangeCameraCharacteristics()
    {
        DeleteNullableObjects(_observedObjects); 
        SetDistanseAndCenterPoint(_observedObjects);

        float xSize = _maxDistadistantBetweenObjects.x - _minDistanceBetweenObjectsForCameraMove.x;
        xSize = xSize > 0 ? xSize : 0;
        float ySize = _maxDistadistantBetweenObjects.y - _minDistanceBetweenObjectsForCameraMove.y;
        ySize = ySize > 0 ? ySize : 0;
        _currentAddtitionalSize = new Vector2(xSize, ySize);
        if ((_previousAddtitionalSize - _currentAddtitionalSize).magnitude > _changeEpsilon ||
            (_previousCenterPosition - _ñurrentCenterPosition).magnitude > _changeEpsilon)
        {
            MoveCamera(_previousCenterPosition, _ñurrentCenterPosition, 1);
            ResizeCamera(_previousAddtitionalSize, _currentAddtitionalSize, 1);

        }
        _previousCenterPosition = _ñurrentCenterPosition;
        _previousAddtitionalSize = _currentAddtitionalSize;
    }

    private void DeleteNullableObjects(List<GameObject> observedObjects)
    {
        List<GameObject> needToRemove = new List<GameObject>();
        foreach (var obj in observedObjects)
            if (obj == null)
                needToRemove.Add(obj);

        foreach (var obj in needToRemove)
            observedObjects.Remove(obj);
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
        _camera.orthographicSize = Mathf.Clamp(_minCameraSize + addSize, _minCameraSize, _maxCameraSize);
    }

}
