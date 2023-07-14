using UnityEditor;
using UnityEngine;

namespace Merge2D.Source
{
    [CustomEditor(typeof(ItemSpawnPoint))]
    public class ItemSpawnPointEditor : Editor
    {
        private const float SphereRadius = .5f;
        
        private void OnSceneGUI()
        {
            var spawnPoint = (ItemSpawnPoint) target;
            Vector3 position = spawnPoint.transform.position;

            Handles.color = Color.yellow;
            Handles.DrawWireArc(position, Vector3.forward, Vector3.right, 360, SphereRadius);
        }

        [DrawGizmo(GizmoType.NonSelected)]
        private static void DrawNonSelected(ItemSpawnPoint spawnPoint, GizmoType gizmoType)
        {
            Vector3 position = spawnPoint.transform.position;
            var name = $"SpawnPoint: {spawnPoint.Type}";

            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(position, SphereRadius);
            
            DrawString(name, position, Color.black, new Vector2(0f, 4f), 10);
        }

        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        private static void DrawSelected(ItemSpawnPoint spawnPoint, GizmoType gizmoType)
        {
            Vector3 position = spawnPoint.transform.position;
            var name = $"SpawnPoint: {spawnPoint.Type}";
            
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(position, SphereRadius);
            
            DrawString(name, position, Color.black, new Vector2(0f, 4f), 14);
        }

        private static void DrawString(string text, Vector3 worldPosition, Color textColor, Vector2 anchor,
            float textSize = 15f)
        {
            var view = SceneView.currentDrawingSceneView;
            if (!view)
                return;

            Vector3 screenPosition = view.camera.WorldToScreenPoint(worldPosition);
            if (screenPosition.y < 0 || screenPosition.y > view.camera.pixelHeight || screenPosition.x < 0 ||
                screenPosition.x > view.camera.pixelWidth || screenPosition.z < 0)
                return;
            
            var pixelRatio = HandleUtility.GUIPointToScreenPixelCoordinate(Vector2.right).x -
                             HandleUtility.GUIPointToScreenPixelCoordinate(Vector2.zero).x;
         
            Handles.BeginGUI();
            
            var style = new GUIStyle(GUI.skin.label)
            {
                fontSize = (int) textSize,
                normal = new GUIStyleState {textColor = textColor}
            };
            Vector2 size = style.CalcSize(new GUIContent(text)) * pixelRatio;
            var alignedPosition =
                ((Vector2) screenPosition +
                 size * ((anchor + Vector2.left + Vector2.up) / 2f)) * (Vector2.right + Vector2.down) +
                Vector2.up * view.camera.pixelHeight;
            GUI.Label(new Rect(alignedPosition / pixelRatio, size / pixelRatio), text, style);
            
            Handles.EndGUI();
        }
    }
}