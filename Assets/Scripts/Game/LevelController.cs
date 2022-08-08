

using System;
using System.Collections.Generic;
using Units;
using UnityEngine;
using Object = UnityEngine.Object;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private List<Spike> _spikes;
    [SerializeField] private List<Door> _doors;

    public event Action LevelDone;
    public event Action LevelLosed;
    private void OnEnable()
    {
        _player.Destroyed += LoseLevel;
        foreach (var door in _doors)
            door.ReachedDoor += EnterDoor;
        foreach (var spike in _spikes)
            spike.SpikesTouched += InflictSpikesHit;
    }
    private void OnDisable()
    {
        _player.Destroyed -= LoseLevel;
        foreach (var door in _doors)
            door.ReachedDoor -= EnterDoor;
        foreach (var spike in _spikes)
            spike.SpikesTouched += InflictSpikesHit;
    }

    private void EnterDoor(Unit unit)
    {
        if (unit.TryGetComponent<Enemy>(out Enemy enemy))
            LoseLevel();
        if (unit.TryGetComponent<Player>(out Player player))
            WinLevel();
    }

    private void InflictSpikesHit(Unit unit)
    {
        Destroy(unit.gameObject);
    }

    private void LoseLevel()
    {
        LevelLosed?.Invoke();
    }

    private void WinLevel()
    {
        LevelDone?.Invoke();
    }
}
