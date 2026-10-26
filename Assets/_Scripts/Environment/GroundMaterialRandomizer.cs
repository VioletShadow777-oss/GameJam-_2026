using UnityEngine;

public class GroundMaterialRandomizer : MonoBehaviour
{
    private MeshRenderer groundMeshRenderer;

    [Space]
    [Header("Ground Materials")]
    public Material[] groundMaterials;

    private int index;
    private void Awake()
    {
        groundMeshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        MaterialRandomize();


    }

    private void MaterialRandomize()
    {
        index = Random.Range(0, groundMaterials.Length);
        groundMeshRenderer.material = groundMaterials[index];
    }
}
