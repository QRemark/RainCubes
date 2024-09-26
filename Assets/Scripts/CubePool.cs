using UnityEngine;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 15;

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    public GameObject GetCube() => _pool.Get();

    public void ReleaseCube(GameObject cube) => _pool.Release(cube);
}
