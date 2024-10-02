using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    [SerializeField] private CubeColorChanger _colorChananger;

    private Renderer _renderer;

    private Action<Cube> _timeEnd;

    private bool _isColorChanged = false;

    private float _lifeTime;
    private float _minLifeTimer = 2.0f;
    private float _maxLifeTimer = 5.0f;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Init(Color initialColor, Action<Cube> timeEnd)
    {
        _isColorChanged = false;
        _renderer.material.color = initialColor;
        _timeEnd = timeEnd;
    }

    public void OnPlatformCollision()
    {
        if (_isColorChanged == false)
        {
            _isColorChanged = true;

            _colorChananger.ChangeColor(this);

            StartLifeTimer();
        }
    }

    private void StartLifeTimer()
    {
        _lifeTime = UnityEngine.Random.Range(_minLifeTimer, _maxLifeTimer + 1.0f);
        Invoke(nameof(NotifyTimeEnd), _lifeTime);
    }

    private void NotifyTimeEnd()
    {
        _timeEnd?.Invoke(this);
    }
}
