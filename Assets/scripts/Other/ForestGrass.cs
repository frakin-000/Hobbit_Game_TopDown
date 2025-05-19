#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ForestGrass : MonoBehaviour
{
    public Vector2 cellSize = Vector2.one;

#if UNITY_EDITOR
    [ContextMenu("Разложить детей по сетке")]
    private void AlignChildren()
    {
        int childCount = transform.childCount;
        int width = Mathf.CeilToInt(Mathf.Sqrt(childCount));

        for (int i = 0; i < childCount; i++)
        {
            int x = i % width;
            int y = i / width;
            Transform child = transform.GetChild(i);
            child.localPosition = new Vector3(x * cellSize.x, -y * cellSize.y, 0);
        }

        Debug.Log("Объекты размещены!");
    }
#endif
}