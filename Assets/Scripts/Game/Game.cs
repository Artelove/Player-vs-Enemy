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
    private PressButton[] _pressButtons;
    private Gate[] _gates;
    private void Awake()
    {
        _player = GetComponentInChildren<Player>();
        _enemy = GetComponentInChildren<Enemy>();
        _exitDoor = GetComponentInChildren<Door>();

        _spikes = GetComponentsInChildren<Spike>();
        _pressButtons = GetComponentsInChildren<PressButton>();
        _gates = GetComponentsInChildren<Gate>();
    }
    private void OnEnable()
    {
        _player.ObjectDestroyed += LoseLevel;
        _exitDoor.ReachedDoor += EnterDoor;
        foreach (var spike in _spikes)
            spike.SpikesTouched += InflictSpikesHit;

        foreach (var pressButton in _pressButtons)
        {
            pressButton.ButtonPressed += LowerGate;
            pressButton.ButtonUnPressed += RaiseGate;
        }
    }
    private void OnDisable()
    {
        _player.ObjectDestroyed -= LoseLevel;
        _exitDoor.ReachedDoor -= EnterDoor;
        foreach (var spike in _spikes)
            spike.SpikesTouched += InflictSpikesHit;
    }
    private void EnterDoor(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            LoseLevel();
        if (gameObject.TryGetComponent<Player>(out Player player))
            WinLevel();
    }
    private void LowerGate(int id)
    {
        foreach (var gate in _gates)
        {
            if (gate.ButtonTriggerId == id)
                gate.LowerGate();
        }
    }
    private void RaiseGate(int id)
    {
        foreach (var gate in _gates)
        {
            if (gate.ButtonTriggerId == id)
                gate.RaiseGate();
        }
    }
    private void InflictSpikesHit(GameObject gameObject)
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
