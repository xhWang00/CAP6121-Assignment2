using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;
    public AudioClip sliceSound;

    public Material crossMaterial;
    public float cutForce = 2000f;

    private GameObject audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startPoint.position, endPoint.position, out RaycastHit hit, sliceableLayer);
        if (hasHit) {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target) {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endPoint.position - startPoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endPoint.position, planeNormal);

        if (hull != null) {
            if (crossMaterial == null) {
                crossMaterial = target.GetComponent<Renderer>().material;
            }

            GameObject upperHull = hull.CreateUpperHull(target, crossMaterial);
            SetupSlicedComp(upperHull);

            Destroy(upperHull, 5f);

            GameObject lowerHull = hull.CreateLowerHull(target, crossMaterial);
            SetupSlicedComp(lowerHull);

            Destroy(lowerHull, 5f);

            AudioSource audioSource = audioPlayer.GetComponent<AudioSource>();
            audioSource.clip = sliceSound;
            audioSource.Play();

            Destroy(target);
        }
    }

    public void SetupSlicedComp(GameObject slicedObject) {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
