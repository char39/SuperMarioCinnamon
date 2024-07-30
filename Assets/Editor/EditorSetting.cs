using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorSetting : MonoBehaviour
{
    [MenuItem("Tools/Change Selected Sprites Pivot")]
    [System.Obsolete]
    private static void ChangeSelectedSpritesPivot()
    {
        // Define the new pivot point
        Vector2 newPivot = new Vector2(0.5f, 0f); // pivot

        // Get selected objects in the editor
        Object[] selectedObjects = Selection.objects;

        foreach (Object obj in selectedObjects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

            if (importer != null)
            {
                if (importer.spriteImportMode == SpriteImportMode.Multiple)
                {
                    SpriteMetaData[] spritesheet = importer.spritesheet;
                    for (int i = 0; i < spritesheet.Length; i++)
                    {
                        spritesheet[i].pivot = newPivot;
                        Debug.Log($"Changed pivot for sprite {spritesheet[i].name} in {path}");
                    }
                    importer.spritesheet = spritesheet;
                    importer.SaveAndReimport();
                    Debug.Log($"Changed pivot for multiple sprites: {path}");
                }
                else if (importer.spriteImportMode == SpriteImportMode.Single)
                {
                    importer.spritePivot = newPivot;
                    importer.SaveAndReimport();
                    Debug.Log($"Changed pivot for single sprite: {path}");
                }
            }
            else
            {
                Debug.LogWarning($"Importer is null for path: {path}");
            }
        }

        Debug.Log("Selected sprite pivots have been changed.");
    }
}
