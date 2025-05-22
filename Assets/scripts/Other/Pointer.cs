using UnityEngine;

[ExecuteAlways]
public class Pointer : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private Camera camera;
    [SerializeField] private Transform pointerIconTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var fromPlayerToTarget = transform.position - player.position;
        var ray = new Ray(player.position, fromPlayerToTarget);
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        var minDistance = Mathf.Infinity;
        for (var i = 0; i < 4; i++)
        {
            if(planes[i].Raycast(ray, out var distance))
            {
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
        }
        minDistance = Mathf.Clamp(minDistance, 0, fromPlayerToTarget.magnitude);
        var worldPosition = ray.GetPoint(minDistance);
        pointerIconTransform.position = camera.WorldToScreenPoint(worldPosition);
        
        
    }
}
