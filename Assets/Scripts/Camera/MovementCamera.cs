using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MovementCamera : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;

    [SerializeField] private float _minCameraSize;
    [SerializeField] private float _distanceBetweenObjectsX;
    [SerializeField] private float _distanceBetweenObjectsY;
    [SerializeField] private float _rechangeTimeDuration;
    [SerializeField] private float _rechangeTimeDelay;

    private Camera _camera;
    
    private float centerPointX;
    private float centerPointY;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _camera.orthographicSize = _minCameraSize;
        StartCoroutine(ChangeCameraCharacteristics(_rechangeTimeDuration));
    }
    private float getMaxDistanseXAndSetCenterPoint(IEnumerable<GameObject> objects)
    {
        float max = 0;
        float distanse = 0;
        foreach (GameObject obj1 in objects)
        {
            foreach (GameObject obj2 in objects)
            {
                distanse = Mathf.Abs(obj1.transform.position.x - obj2.transform.position.x);
                if (distanse > max)
                {
                    centerPointX = (obj1.transform.position.x + obj2.transform.position.x) / 2f;
                    max = distanse;
                }
                    
            }
        }
        return max;
    }

    private float getMaxDistanseYAndSetCenterPoint(IEnumerable<GameObject> objects)
    {
        float max = 0;
        float distanse = 0;
        foreach (GameObject obj1 in objects)
        {
            foreach (GameObject obj2 in objects)
            {
                distanse = Mathf.Abs(obj1.transform.position.y - obj2.transform.position.y);
                if (distanse > max)
                {
                    centerPointY = (obj1.transform.position.y + obj2.transform.position.y) / 2f;
                    max = distanse;
                }

            }
        }
        return max;
    }

    private IEnumerator ChangeCameraCharacteristics(float changeTimeDuration)
    {
        float changeTime = 0;
        Vector2 currentAddtitionalSize;
        Vector2 previousAddtitionalSize = new Vector2(0,0);
        float distanceX = getMaxDistanseXAndSetCenterPoint(_objects);
        float distanceY = getMaxDistanseYAndSetCenterPoint(_objects);
        float centerX = centerPointX;
        float centerY = centerPointY;

        Vector2 currentCenterPosition;
        Vector2 previousCenterPosition = new Vector2(centerX, centerY);
        while (true) {
            distanceX = getMaxDistanseXAndSetCenterPoint(_objects);
            distanceY = getMaxDistanseYAndSetCenterPoint(_objects);

            currentAddtitionalSize = new Vector2(distanceX - _distanceBetweenObjectsX > 0 ? (distanceX - _distanceBetweenObjectsX) : 0, 
                                                 distanceY - _distanceBetweenObjectsY > 0 ? (distanceY - _distanceBetweenObjectsY) : 0);
            currentCenterPosition = new Vector2(centerPointX, centerPointY);

            changeTime = 0;
            while (changeTime < changeTimeDuration)
            {
                MoveCamera(previousCenterPosition, currentCenterPosition, changeTime / changeTimeDuration);
                ResizeCamera(previousAddtitionalSize, currentAddtitionalSize, changeTime / changeTimeDuration);
                changeTime += Time.deltaTime;
                yield return null;
            }
            previousAddtitionalSize = currentAddtitionalSize;
            previousCenterPosition = currentCenterPosition;
        }
    }

    private void MoveCamera(Vector2 previousAddtitionalSize, Vector2 currentCenterPosition, float coefficient)
    {
        Vector2 center = Vector2.Lerp(previousAddtitionalSize, currentCenterPosition, coefficient);
        _camera.transform.position = new Vector3(center.x, center.y, _camera.transform.position.z);
    }

    private void ResizeCamera(Vector2 previusCenterPosition, Vector2 currentAddtitionalSize, float coefficient)
    {

        Vector2 additionalSize = Vector2.Lerp(previusCenterPosition, currentAddtitionalSize, coefficient);
        float addSize = additionalSize.x * (0.25f) > additionalSize.y * (0.4f) ? additionalSize.x * (0.25f) : additionalSize.y * (0.4f);
        _camera.orthographicSize = _minCameraSize + addSize;
    }

}
