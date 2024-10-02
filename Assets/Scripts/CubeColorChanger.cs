using UnityEngine;

public class CubeColorChanger : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ChangeColor(Cube cube)
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        _renderer.material.color = randomColor;
    }
}