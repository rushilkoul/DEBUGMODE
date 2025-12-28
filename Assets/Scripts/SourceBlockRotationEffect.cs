using UnityEngine;

public class SourceBlockRotationEffect : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(30f, 45f, 60f);

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
