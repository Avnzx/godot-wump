using Godot;
using System;


// Handy for debugging, allows us to see a 3D vector visualised onto
// Screen space, as Godot cannot do that
class VectorVisualise : Control {

    private CanvasLayer DebugCanvasLayer;

    // We are translating the 3D vector into 2D to be able
    // To draw it
    public VectorVisualise(Node node) {
        DebugCanvasLayer = new CanvasLayer();
        DebugCanvasLayer.Name = "DebugCanvas";
        this.Name = "Vector Visualisation";
        
        // Put this node into the scene tree
        node.AddChild(DebugCanvasLayer, true);
        DebugCanvasLayer.AddChild(this,true);
    }

    // Draw (line, poly, etc...) calls can ONLY be made in this method
    public override void _Draw() => DrawQueue();
    

    // It schedules _Draw in a roundabout way
    public override void _Process(float delta) => Update();


    // Helper function to draw a simple triangle
    public void DrawTriangle
    (Vector2 pos, Vector2 dir, float size, Color color) {

    Vector2 a = pos + dir * size;
    Vector2 b = pos + dir.Rotated(2*Mathf.Pi/3) * size;
    Vector2 c = pos + dir.Rotated(4*Mathf.Pi/3) * size;

    //Vector2[] points = new Vector2[3];
    Vector2[] points = {a, b, c};
    DrawPolygon(points, new Color[] {color});
    }

    // Draw all vectors in the queue to screen
    public void DrawQueue() {

        Camera camera = this.GetViewport().GetCamera();
        float _width = 10f;
        float _scale = 100f;

        for (int i = 0; i < visualise_queue.Count; i++) {
            Godot.Collections.Array _arr = (Godot.Collections.Array) visualise_queue[i];
            Godot.Spatial _obj = (Godot.Spatial) _arr[0];
            Vector3 _property = (Vector3) _arr[1];
            Color _color = (Color) _arr[2];

            var start = camera.UnprojectPosition(_obj.GlobalTransform.origin);
            var end = camera.UnprojectPosition(_obj.GlobalTransform.origin + (Vector3) _property * _scale);
            DrawLine(start, end, _color, _width);
            DrawTriangle(end, start.DirectionTo(end), _width*2, _color);
        }
        
    }

    Godot.Collections.Array visualise_queue = new Godot.Collections.Array();

    // Add some object to the visualise queue
    // Where _obj will be the origin and _prop
    // Is the visualised vector
    public void AddVisQueue(Godot.Spatial _obj, Vector3 _prop) {
        Godot.Collections.Array _newthing = new Godot.Collections.Array();
        _newthing.Add(_obj);
        _newthing.Add(_prop);
        _newthing.Add(new Color(0,1,1,1));
        visualise_queue.Add(_newthing);
    }

    public void AddVisQueue(Godot.Spatial _obj, Vector3 _prop, Color _color) {
        Godot.Collections.Array _newthing = new Godot.Collections.Array();
        _newthing.Add(_obj);
        _newthing.Add(_prop);
        _newthing.Add(_color);
        visualise_queue.Add(_newthing);
    }


    // Remove the last element from the visualise queue
    public void PopLastVisual() => visualise_queue.RemoveAt(visualise_queue.Count - 1);

    // Return the visualisation queue
    public Godot.Collections.Array GetVisQueue() => visualise_queue;

}