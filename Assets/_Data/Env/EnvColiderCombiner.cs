using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnvColiderCombiner : PMono
{
    [SerializeField] protected List<MeshFilter> meshFilters = new();

    protected override void Awake()
    {
        this.CombineMeshesAndAddColliderRecursively(transform.gameObject);
    }

    protected override void Start()
    {
        this.LoadMesh();
    }

    protected override void LoadComponents()
    {
        this.LoadMesh();
    }

    protected virtual void LoadMesh()
    {
        this.meshFilters = new List<MeshFilter>();
        this.meshFilters = transform.GetComponentsInChildren<MeshFilter>().ToList<MeshFilter>();

    }

    //protected virtual void CombineMeshesAndAddCollider(GameObject parent)
    //{
    //    MeshFilter[] this.meshFilters = parent.GetComponentsInChildren<MeshFilter>();
    //    CombineInstance[] combine = new CombineInstance[this.meshFilters.Length];

    //    for (int i = 0; i < this.meshFilters.Length; i++)
    //    {
    //        combine[i].mesh = this.meshFilters[i].sharedMesh;
    //        combine[i].transform = this.meshFilters[i].transform.localToWorldMatrix;
    //    }

    //    Mesh combinedMesh = new Mesh();
    //    combinedMesh.CombineMeshes(combine);

    //    MeshFilter mf = parent.AddComponent<MeshFilter>();
    //    mf.mesh = combinedMesh;

    //    MeshCollider mc = parent.AddComponent<MeshCollider>();
    //    mc.sharedMesh = combinedMesh;

    //    foreach (Transform child in parent.transform)
    //    {
    //        Destroy(child.GetComponent<MeshRenderer>());
    //        Destroy(child.GetComponent<Collider>());
    //    }
    //}

    protected virtual void CombineMeshesAndAddColliderRecursively(GameObject parent)
    {
        this.meshFilters = parent.GetComponentsInChildren<MeshFilter>().ToList<MeshFilter>();
        var combine = new CombineInstance[this.meshFilters.Count];
        int index = 0;

        Vector3 originalPosition = parent.transform.position;
        parent.transform.position = Vector3.zero;

        foreach (MeshFilter meshFilter in this.meshFilters)
        {
            if (meshFilter == null || meshFilter.sharedMesh == null) continue;

            combine[index].mesh = meshFilter.sharedMesh;
            combine[index].transform = meshFilter.transform.localToWorldMatrix;
            index++;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        combinedMesh.CombineMeshes(combine, true, true);

        MeshFilter parentMeshFilter = parent.GetComponent<MeshFilter>();
        if (parentMeshFilter == null) parentMeshFilter = parent.AddComponent<MeshFilter>();
        parentMeshFilter.mesh = combinedMesh;

        MeshRenderer parentMeshRenderer = parent.GetComponent<MeshRenderer>();
        if (parentMeshRenderer == null) parentMeshRenderer = parent.AddComponent<MeshRenderer>();
        parentMeshRenderer.material = this.meshFilters[0]?.GetComponent<MeshRenderer>()?.sharedMaterial;

        MeshCollider parentCollider = parent.GetComponent<MeshCollider>();
        if (parentCollider == null) parentCollider = parent.AddComponent<MeshCollider>();
        parentCollider.sharedMesh = combinedMesh;

        foreach (Transform child in parent.transform)
        {
            DestroyRecursive(child);
        }

        this.LoadMesh();
        parent.transform.position = originalPosition;
    }

    protected virtual void DestroyRecursive(Transform obj)
    {
        var meshFilter = obj.GetComponent<MeshFilter>();
        if (meshFilter != null) Destroy(meshFilter);

        var meshRenderer = obj.GetComponent<MeshRenderer>();
        if (meshRenderer != null) Destroy(meshRenderer);

        var collider = obj.GetComponent<Collider>();
        if (collider != null) Destroy(collider);

        foreach (Transform child in obj)
        {
            DestroyRecursive(child);
        }
    }

}
