using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    private Player _player;
    private Enemy _enemy;
    private Door _exitDoor;
    private Spike[] _spikes;
    private void Awake()
    {
        _player = GetComponentInChildren<Player>();
        _enemy = GetComponentInChildren<Enemy>();
        _exitDoor = GetComponentInChildren<Door>();

        _spikes = GetComponentsInChildren<Spike>();
    }
    private void OnEnable()
    {
        _player.ObjectDestroyed += LoseLevel;
        _exitDoor.ReachedDoor += EnterDoor;
        foreach (var spike in _spikes)
        {
            spike.SpikesTouched += TakeSpikesHit;
        }
    }
    private void OnDisable()
    {
        _player.ObjectDestroyed -= LoseLevel;
        _exitDoor.ReachedDoor -= EnterDoor;
        foreach (var spike in _spikes)
        {
            spike.SpikesTouched += TakeSpikesHit;
        }
    }
    private void EnterDoor(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            LoseLevel();
        if (gameObject.TryGetComponent<Player>(out Player player))
            WinLevel();
    }

    private void TakeSpikesHit(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    private void LoseLevel()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    private void WinLevel()
    {
        Debug.Log("Win");
    }
}
