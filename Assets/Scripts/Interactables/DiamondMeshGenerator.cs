using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomMesh
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class DiamondMeshGenerator : MonoBehaviour
    {
        private Transform m_transform;

        void Awake()
        {
            m_transform = GetComponent<Transform>();
        }

        // le mesh du Diamand sera généré ici, triangle par triangle
        void OnEnable()
        {
            // Création du Mesh
            var mesh = new Mesh
            {
                name = "Diamond Mesh"
            };

            // Définition de la position de chaque sommets
            mesh.vertices = new Vector3[]
            {
                Vector3.zero, Vector3.right, Vector3.up, Vector3.one
            };

            // Direction des vecteurs normaux pour chaque sommets
            mesh.normals = new Vector3[]
            {
                Vector3.back, Vector3.back, Vector3.back, Vector3.back
            };

            // Définition de tous les triangles du Mesh. Chaque trio de valeurs consiste à l'indice de chaque sommets de ce triangle, définis auparavant.
            // Pour que le triangle s'affiche dans le bon sens, penser à avoir l'ordre des sommets dans le sens des aiguilles d'une montre visuellement.
            mesh.triangles = new int[] {
                0, 2, 1,
                1, 2, 3
            };

            GetComponent<MeshFilter>().mesh = mesh;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            m_transform.Rotate(0, Time.deltaTime * -20, 0);
        }
    }
}