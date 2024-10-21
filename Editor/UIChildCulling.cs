using System.Collections.Generic;
using UnityEngine;

namespace AKRN_Utilities
{
    [RequireComponent(typeof(RectTransform))]
    public class UIChildCulling : MonoBehaviour
    {
        private RectTransform parentRectTransform;
        public List<GameObject> exceptions;

        void Start()
        {
            parentRectTransform = GetComponent<RectTransform>();
        }

        void Update()
        {
            ToggleActiveObjects();
        }

        void ToggleActiveObjects()
        {
            RectTransform[] allChildren = GetComponentsInChildren<RectTransform>(true);

            foreach (RectTransform rectTransform in allChildren)
            {
                if (rectTransform == parentRectTransform || exceptions.Contains(rectTransform.gameObject))
                {
                    continue;
                }

                if (IsRectTransformInParentView(rectTransform))
                {
                    if (!rectTransform.gameObject.activeSelf)
                    {
                        rectTransform.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (rectTransform.gameObject.activeSelf)
                    {
                        rectTransform.gameObject.SetActive(false);
                    }
                }
            }
        }

        bool IsRectTransformInParentView(RectTransform rectTransform)
        {
            Vector3[] childCorners = new Vector3[4];
            rectTransform.GetWorldCorners(childCorners);

            Vector3[] parentCorners = new Vector3[4];
            parentRectTransform.GetWorldCorners(parentCorners);

            Rect parentRect = new Rect(
                parentCorners[0].x,
                parentCorners[0].y,
                parentCorners[2].x - parentCorners[0].x,
                parentCorners[2].y - parentCorners[0].y
            );

            foreach (Vector3 corner in childCorners)
            {
                if (parentRect.Contains(corner))
                {
                    return true;
                }
            }

            return false;
        }
    }
}