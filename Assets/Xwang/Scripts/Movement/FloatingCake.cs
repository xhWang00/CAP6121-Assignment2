using UnityEngine;

public class FloatObject : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + amplitude * new Vector3(0, Mathf.Sin(Time.time * frequency), 0);
    }
}