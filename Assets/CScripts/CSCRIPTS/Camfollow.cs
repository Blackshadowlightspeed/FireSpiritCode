using UnityEngine;

public class Camfollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float followSpeed = 5f;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, followSpeed * Time.deltaTime);
    }
}

