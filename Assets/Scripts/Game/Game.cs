using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    private Door _exitDoor;

    private UnityEvent<GameObject> _levelFinished = new UnityEvent<GameObject>();

    public event UnityAction<GameObject> LevelFinished
    {
        add => _levelFinished.AddListener(value);
        remove => _levelFinished.RemoveListener(value);
    }
    private void OnEnable()
    {
        _exitDoor = GetComponentInChildren<Door>();
        _exitDoor.ReachedDoor += EnterDoor;
        foreach (var item in GetComponentsInChildren<Spike>())
        {
            item.SpikesTouched += TakeSpikesHit;
        }
    }
    private void OnDisable()
    {
        _exitDoor = GetComponentInChildren<Door>();
        _exitDoor.ReachedDoor -= EnterDoor;
        foreach (var item in GetComponentsInChildren<Spike>())
        {
            item.SpikesTouched -= TakeSpikesHit;
        }
    }

    private void EnterDoor(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            Debug.Log("Win");
        if (gameObject.TryGetComponent<Player>(out Player player))
            Debug.Log("Win");
    }

    private void TakeSpikesHit(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            Debug.Log("Lose");
        if (gameObject.TryGetComponent<Player>(out Player player))
            Debug.Log("Win");
    }
}
