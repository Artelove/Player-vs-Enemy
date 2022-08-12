

using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Units;
using UnityEngine;
using Object = UnityEngine.Object;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject _spikesContaiter;
    [SerializeField] private List<Door> _doors;
    
    public event Action LevelDone;
    public event Action LevelLosed;
    private void OnEnable()
    {
        _player.Destroyed += LoseLevel;
        _enemy.PlayerTounched += InflictEnemyHit;
        foreach (var door in _doors)
            door.ReachedDoor += EnterDoor;
        foreach (var spike in _spikesContaiter.GetComponentsInChildren<Spike>())
            spike.SpikesTouched += InflictSpikesHit;
    }
    private void OnDisable()
    {
        _player.Destroyed -= LoseLevel;
        _enemy.PlayerTounched -= InflictEnemyHit;
        foreach (var door in _doors)
            door.ReachedDoor -= EnterDoor;
        foreach (var spike in _spikesContaiter.GetComponentsInChildren<Spike>())
            spike.SpikesTouched -= InflictSpikesHit;
    }

    private void EnterDoor(Unit unit)
    {
        unit.transform.DOMove(_doors[0].transform.position,0.5f);
        if (unit.TryGetComponent<Enemy>(out Enemy enemy))
            LoseLevel();
        if (unit.TryGetComponent<Player>(out Player player))
            WinLevel();
    }

    private void InflictSpikesHit(Unit unit)
    {
        unit.gameObject.GetComponent<Unit>().Destroy(0);
    }

    private void InflictEnemyHit()
    {
        _player.gameObject.GetComponent<Unit>().Destroy(0);
    }

    private void LoseLevel()
    {
        EndLevel();
        LevelLosed?.Invoke();
    }

    private void WinLevel()
    {
        EndLevel();
        LevelDone?.Invoke();
    }

    private void EndLevel()
    {
        if(_player!=null) _player.gameObject.GetComponent<Unit>().Destroy(0.5f);
        if(_enemy!=null) _enemy.gameObject.GetComponent<Unit>().Destroy(0.5f);
        GetComponent<LevelController>().enabled = false;
    }
}
