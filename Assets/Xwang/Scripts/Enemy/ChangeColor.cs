using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color newColor;

    void Start()
    {
        GameObject child = transform.Find("Cactus").gameObject;
        if (child != null)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = newColor;
            }
            else
            {
                Debug.Log("No Renderer component found on Cactus.");
            }
        }
        else
        {
            Debug.Log("No child GameObject named Cactus found.");
        }
    }
}