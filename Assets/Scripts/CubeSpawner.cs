using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{ 
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private Cube _prefab;
    
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 15;

    [SerializeField] private float _repeatRate = 0.5f;
    [SerializeField] private float _radiusX = 8f;
    [SerializeField] private float _radiusZ = 8f;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => cube.gameObject.SetActive(true),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCube), 0.0f, _repeatRate);
    }

    private Cube GetCube()
    {
        Cube cube = _pool.Get();

        cube.Init(Color.white, ReturnCubeInPool);

        return cube;
    }

    private void SpawnCube()
    {
        Cube cube = GetCube();

        Vector3 spawnPosition = new Vector3(_startPoint.transform.position.x + Random.Range(-_radiusX, _radiusX),
        _startPoint.transform.position.y,
        _startPoint.transform.position.z + Random.Range(-_radiusZ, _radiusZ));

        cube.transform.position = spawnPosition;

        if (cube.TryGetComponent(out Rigidbody cubeRigidbody))
        {
            cubeRigidbody.velocity = Vector3.zero;
        }
    }

    private void ReturnCubeInPool(Cube cube)
    {
        _pool.Release(cube);
    }
}
