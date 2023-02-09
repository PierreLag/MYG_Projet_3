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
                Vector3.zero,                           // 0
                new Vector3(0       , 1     , -1.5f),   // 1
                new Vector3(1       , 1     , -1),      // 2
                new Vector3(1.5f    , 1     , 0),       // 3
                new Vector3(1       , 1     , 1),       // 4
                new Vector3(0       , 1     , 1.5f),    // 5
                new Vector3(-1      , 1     , 1),       // 6
                new Vector3(-1.5f   , 1     , 0),       // 7
                new Vector3(-1      , 1     , -1)       // 8
            };

            // Direction des vecteurs normaux pour chaque sommets
            mesh.normals = new Vector3[]
            {
                Vector3.down,
                new Vector3(0, 0, -1),
                new Vector3(1, 0, -1),
                new Vector3(1, 0, 0),
                new Vector3(1, 0, 1),
                new Vector3(0, 0, 1),
                new Vector3(-1, 0, 1),
                new Vector3(-1, 0, 0),
                new Vector3(-1, 0, -1),
            };

            // Définition de tous les triangles du Mesh. Chaque trio de valeurs consiste à l'indice de chaque sommets de ce triangle, définis auparavant.
            // Pour que le triangle s'affiche dans le bon sens, penser à avoir l'ordre des sommets dans le sens des aiguilles d'une montre visuellement.
            mesh.triangles = new int[] {
                0, 1, 2,
                0, 2, 3,
                0, 3, 4,
                0, 4, 5,
                0, 5, 6,
                0, 6, 7,
                0, 7, 8,
                0, 8, 1
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