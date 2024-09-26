using UnityEngine;

public class Platform : MonoBehaviour
{
    public delegate void CubeCollisionHandler(Cube cube);
    public static event CubeCollisionHandler OnCubeCollision;

    private void OnCollisionEnter(Collision collision)
    {
        Cube cube = collision.gameObject.GetComponent<Cube>();
        if (cube != null)
        {
            OnCubeCollision?.Invoke(cube);
        }
    }
}
