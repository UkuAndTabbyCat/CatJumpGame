using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToonNumbers
{
    public class NLcopyBlendShapes : MonoBehaviour
    {
        public GameObject REfObject;
        public int BlendShapesN;
        SkinnedMeshRenderer REFskinnedMeshRenderer;
        SkinnedMeshRenderer skinnedMeshRenderer;

        void Start()
        {
            REFskinnedMeshRenderer = REfObject.GetComponent<SkinnedMeshRenderer>();
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        void Update()
        {
            for (int forAUX = 0; forAUX < BlendShapesN; forAUX++)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(forAUX, REFskinnedMeshRenderer.GetBlendShapeWeight(forAUX));
            }
        }
    }
}
