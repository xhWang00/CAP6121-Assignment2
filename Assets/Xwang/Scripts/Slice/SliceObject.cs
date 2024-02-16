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

    public Material crossMaterial;
    public float cutForce = 2000f;

    // Start is called before the first frame update
    void Start()
    {
        
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
            GameObject lowerHull = hull.CreateLowerHull(target, crossMaterial);
            SetupSlicedComp(lowerHull);

            Destroy(target);
        }
    }

    public void SetupSlicedComp(GameObject slicedObject) {
        // Slicable layer = 6
        slicedObject.layer = 6; 

        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
