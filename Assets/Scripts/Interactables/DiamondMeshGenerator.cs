using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class DiamondMeshGenerator : MonoBehaviour
    {
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
                new Vector3(-1      , 1     , -1),      // 8

                new Vector3(0.4f    , 1.2f  , -0.8f),   // 9
                new Vector3(0.8f    , 1.2f  , -0.4f),   // 10
                new Vector3(0.8f    , 1.2f  , 0.4f),    // 11
                new Vector3(0.4f    , 1.2f  , 0.8f),    // 12
                new Vector3(-0.4f   , 1.2f  , 0.8f),    // 13
                new Vector3(-0.8f   , 1.2f  , 0.4f),    // 14
                new Vector3(-0.8f   , 1.2f  , -0.4f),   // 15
                new Vector3(-0.4f   , 1.2f  , -0.8f),   // 16

                new Vector3(0       , 1.2f  , 0)        // 17
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

                new Vector3(1, 0, -2),
                new Vector3(2, 0, -1),
                new Vector3(2, 0, 1),
                new Vector3(1, 0, 2),
                new Vector3(-1, 0, 2),
                new Vector3(-2, 0, 1),
                new Vector3(-2, 0, -1),
                new Vector3(-1, 0, -2),

                Vector3.up
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
                0, 8, 1,

                1, 9, 2,
                9, 10, 2,
                2, 10, 3,
                10, 11, 3,
                3, 11, 4,
                11, 12, 4,
                4, 12, 5,
                12, 13, 5,
                5, 13, 6,
                13, 14, 6,
                6, 14, 7,
                14, 15, 7,
                7, 15, 8,
                15, 16, 8,
                8, 16, 1,
                16, 9, 1,

                9, 17, 10,
                10, 17, 11,
                11, 17, 12,
                12, 17, 13,
                13, 17, 14,
                14, 17, 15,
                15, 17, 16,
                16, 17, 9
            };

            GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}