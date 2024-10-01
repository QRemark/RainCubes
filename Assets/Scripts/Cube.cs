using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private CubeColorChanger _colorChananger;

    private CubePool _cubePool;
    private Renderer _renderer;

    private bool _isColorChanged = false;

    private float _lifeTime;

    private float _minLifeTimer = 2.0f;
    private float _maxLifeTimer = 5.0f;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Init(CubePool cubePool, Color initialColor)
    {
        _cubePool = cubePool;
        _isColorChanged = false;
        _renderer.material.color = initialColor;
    }

    private void OnEnable()
    {
        Platform.OnCubeCollision += HandlePlatformCollision;
    }

    private void OnDisable()
    {
        Platform.OnCubeCollision -= HandlePlatformCollision;
    }

    private void HandlePlatformCollision(Cube cube)
    {
        if (cube == this && _isColorChanged == false)
        {
            _isColorChanged = true;

            _colorChananger.ChangeColor(this);

            StartLifeTimer();
        }
    }

    private void StartLifeTimer()
    {
        _lifeTime = Random.Range(_minLifeTimer, _maxLifeTimer + 1.0f);
        Invoke(nameof(ReturnToPool), _lifeTime);
    }

    private void ReturnToPool()
    {
        _cubePool.ReleaseCube(gameObject);
    }
}
