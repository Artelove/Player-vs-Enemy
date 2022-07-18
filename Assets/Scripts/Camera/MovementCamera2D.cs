using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MovementCamera2D : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects;

    [SerializeField] private float _minCameraSize;
    [SerializeField] private Vector2 _minDistanceBetweenObjectsToNeedMove;
    [SerializeField] private float _changeTimeDuration;
    [SerializeField] private float _changeTimeDelay;

    private Camera _camera;

    private Vector2 _centerPoint;
    private Vector2 _maxDistadistantBetweenObjects;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _camera.orthographicSize = _minCameraSize;
        StartCoroutine(ChangeCameraCharacteristics(_changeTimeDuration, _changeTimeDelay));
    }
    private void SetDistanseAndCenterPoint()
    {
        Vector2 max = new Vector2(-1,-1);
        Vector2 distanse = new Vector2();
        List<GameObject> needToRemove = new List<GameObject>();
        foreach (var obj in _objects)
            if(obj==null)
                needToRemove.Add(obj);
        foreach (var obj in needToRemove)
            _objects.Remove(obj);


        foreach (var obj1 in _objects)
        {
            foreach (var obj2 in _objects)
            {
                distanse.x = Mathf.Abs(obj1.transform.position.x - obj2.transform.position.x);
                if (distanse.x > max.x)
                {
                    _centerPoint.x = (obj1.transform.position.x + obj2.transform.position.x) / 2f;
                    max.x = distanse.x;
                }
                distanse.y = Mathf.Abs(obj1.transform.position.y - obj2.transform.position.y);
                if (distanse.y > max.y)
                {
                    _centerPoint.y = (obj1.transform.position.y + obj2.transform.position.y) / 2f;
                    max.y = distanse.y;
                }
            }
        }
        
        _maxDistadistantBetweenObjects = max;
    }

    private IEnumerator ChangeCameraCharacteristics(float changeTimeDuration, float _changeTimeDelay)
    {
        var waitDelay = new WaitForSeconds(_changeTimeDelay);
        float changeTime = 0;
        Vector2 currentAddtitionalSize;
        Vector2 previousAddtitionalSize = new Vector2();
        Vector2 currentCenterPosition;
        Vector2 previousCenterPosition = new Vector2();

        while (true) {
            SetDistanseAndCenterPoint();

            float xSize = _maxDistadistantBetweenObjects.x - _minDistanceBetweenObjectsToNeedMove.x;
            xSize = xSize > 0 ? xSize : 0;
            float ySize = _maxDistadistantBetweenObjects.y - _minDistanceBetweenObjectsToNeedMove.y;
            ySize = ySize > 0 ? ySize : 0;
            currentAddtitionalSize = new Vector2(xSize, ySize);

            changeTime = 0;
            while (changeTime < changeTimeDuration)
            {
                MoveCamera(previousCenterPosition, _centerPoint, changeTime / changeTimeDuration);
                ResizeCamera(previousAddtitionalSize, currentAddtitionalSize, changeTime / changeTimeDuration);
                changeTime += Time.deltaTime;
                yield return null;
            }
            previousAddtitionalSize = currentAddtitionalSize;
            previousCenterPosition = _centerPoint;
            yield return waitDelay;
        }
    }

    private void MoveCamera(Vector2 previousAddtitionalSize, Vector2 currentCenterPosition, float coefficient)
    {
        Vector2 center = Vector2.Lerp(previousAddtitionalSize, currentCenterPosition, coefficient);
        _camera.transform.position = new(center.x, center.y, _camera.transform.position.z);
    }

    private void ResizeCamera(Vector2 previusCenterPosition, Vector2 currentAddtitionalSize, float coefficient)
    {
        Vector2 additionalSize = Vector2.Lerp(previusCenterPosition, currentAddtitionalSize, coefficient);
        float addSize = additionalSize.x * (9f/23f) > additionalSize.y * (9f / 16f) ? additionalSize.x * (9f / 23f) : additionalSize.y * (9f / 16f);
        _camera.orthographicSize = _minCameraSize + addSize;
    }

}
