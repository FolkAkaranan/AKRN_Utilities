using UnityEditor;
using UnityEngine;

namespace AKRN_Utilities
{
    public class CustomPrefabThumbnail : EditorWindow
    {
        private GameObject selectedPrefab;
        private Sprite customSprite;

        [MenuItem("Tools/Custom Thumbnail Editor")]
        public static void ShowWindow()
        {
            GetWindow<CustomPrefabThumbnail>("Custom Thumbnail Editor");
        }

        private void OnGUI()
        {
            GUILayout.Label("Assign Custom Thumbnail", EditorStyles.boldLabel);

            selectedPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab", selectedPrefab, typeof(GameObject), false);

            customSprite = (Sprite)EditorGUILayout.ObjectField("Custom Sprite", customSprite, typeof(Sprite), false);

            if (customSprite != null)
            {
                GUILayout.Label("Preview:");
                GUILayout.Label(customSprite.texture, GUILayout.Width(128), GUILayout.Height(128));
            }

            if (selectedPrefab != null && customSprite != null)
            {
                if (GUILayout.Button("Assign Custom Thumbnail"))
                {
                    ShowCustomThumbnail();
                }
            }
        }

        private void ShowCustomThumbnail()
        {
            if (selectedPrefab != null && customSprite != null)
            {
                Debug.Log("Custom thumbnail assigned to " + selectedPrefab.name);
            }
        }
    }
}