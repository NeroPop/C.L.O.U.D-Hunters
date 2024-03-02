namespace Tools.Editor
{
    using UnityEngine;

    [RequireComponent(typeof(MeshCollider))]
    public class DrawMeshColliderGizmo : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            MeshCollider meshCollider = this.GetComponent<MeshCollider>();

            if (meshCollider.sharedMesh == null)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireMesh(meshCollider.sharedMesh, this.transform.position, this.transform.rotation);
        }
    }
}