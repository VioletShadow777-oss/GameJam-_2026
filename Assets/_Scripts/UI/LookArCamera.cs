using UnityEngine;

public class LookArCamera : MonoBehaviour
{
    public Transform camera;

    private void Update()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}
