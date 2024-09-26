using UnityEngine;

public class Cube : MonoBehaviour
{
    private CubePool _cubePool;
    private bool _colorChanged = false;
    private Renderer _renderer;
    private float _lifeTime;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Init(CubePool cubePool, Color initialColor)
    {
        _cubePool = cubePool;
        _colorChanged = false;
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
        if (cube == this && !_colorChanged)
        {
            ChangeColor();
            StartLifeTimer();
        }
    }

    private void ChangeColor()
    {
        _colorChanged = true;

        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }

    private void StartLifeTimer()
    {
        _lifeTime = Random.Range(2f, 6f);
        Invoke(nameof(ReturnToPool), _lifeTime);
    }

    private void ReturnToPool()
    {
        _cubePool.ReleaseCube(gameObject);
    }
}
