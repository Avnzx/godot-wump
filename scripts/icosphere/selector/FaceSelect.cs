using Godot; 

class FaceSelect : Node {

    int selectedface;

    public void OnRoomHover(Godot.Collections.Array properties){
        GD.Print("print");
        selectedface = ((Godot.Collections.Array) sphere!.facecolliders).IndexOf(properties[0]);

        SpatialMaterial mat = new SpatialMaterial();
        mat.AlbedoColor = new Color(1,1,1,1);

        sphere._array_mesh.SurfaceSetMaterial(selectedface, mat);

        for (int i = 0; i < sphere!._array_mesh.GetSurfaceCount(); i++) {
            SpatialMaterial matdefault = new SpatialMaterial();
            matdefault.AlbedoColor = new Color(0.4f,0.1f,0.7f,0.4f);
            
            if (selectedface != i)
                sphere._array_mesh.SurfaceSetMaterial(i, matdefault);
        }
    }

    public override void _PhysicsProcess(float delta) {

    }


    public override void _Ready() {
        sphere = GetNode<IcoSphere>("/root/Icosphere/Sphere");
    }

    IcoSphere? sphere;
}