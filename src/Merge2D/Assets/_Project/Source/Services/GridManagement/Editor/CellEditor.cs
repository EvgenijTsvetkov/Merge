using Merge2D.Source.Services;
using UnityEditor;
using UnityEngine;

namespace Merge2D.Source
{
    [CustomEditor(typeof(Cell))]  
    public class CellEditor : Editor
    {
        [DrawGizmo(GizmoType.NonSelected)]
        private static void NonSelected(Cell cell, GizmoType gizmoType)
        {
            Vector3 position = cell.transform.position;
            string name = $"[{cell.LocalPlace.x}, {cell.LocalPlace.y}]";
            
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(position, 0.4f);
            
            DrawString(name, position, Color.white, new Vector2(2f, 1f), 10);
        }

        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        private static void Selected(Cell cell, GizmoType gizmoType)
        {
            Vector3 position = cell.transform.position;
            string name = $"[{cell.LocalPlace.x}, {cell.LocalPlace.y}]";
            
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(position, 0.4f);
            
            DrawString(name, position, Color.green, new Vector2(2f, 1f), 10);
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