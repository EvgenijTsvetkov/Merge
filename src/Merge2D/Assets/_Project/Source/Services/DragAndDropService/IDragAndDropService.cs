namespace Merge2D.Source.Services
{
    public interface IDragAndDropService
    {
        bool HasDraggableObject { get; }
        IDraggable DraggableObject { get; }
        void BeganDragFor(IDraggable draggableObject);
    }
}