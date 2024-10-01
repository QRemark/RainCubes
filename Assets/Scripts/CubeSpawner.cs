using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private float _repeatRate = 0.5f;
    [SerializeField] private float _radiusX = 8f;
    [SerializeField] private float _radiusZ = 8f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCube), 0.0f, _repeatRate);
    }

    private void SpawnCube()
    {
        GameObject cube = _cubePool.GetCube();

        Vector3 spawnPosition = new Vector3(_startPoint.transform.position.x + Random.Range(-_radiusX, _radiusX),
        _startPoint.transform.position.y,
        _startPoint.transform.position.z + Random.Range(-_radiusZ, _radiusZ));

        cube.transform.position = spawnPosition;

        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;

        cube.GetComponent<Cube>().Init(_cubePool, Color.white);
    }
}
