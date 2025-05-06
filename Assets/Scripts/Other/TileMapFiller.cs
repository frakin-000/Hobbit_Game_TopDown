using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TilemapFiller : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tileToUse;
    public int width = 100;
    public int height = 100;

#if UNITY_EDITOR
    [ContextMenu("Заполнить тайлами")]
    void FillTilemapInEditor()
    {
        if (tilemap == null || tileToUse == null) return;

        Undo.RegisterFullObjectHierarchyUndo(tilemap, "Fill Tilemap");

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), tileToUse);
            }
        }

        EditorUtility.SetDirty(tilemap);
    }
#endif
}