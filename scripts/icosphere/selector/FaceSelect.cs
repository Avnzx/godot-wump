using Godot; 

public class FaceSelect : Node {

    // array containing integer arrats of special faces
    Godot.Collections.Array specialfaces = new Godot.Collections.Array();
    Godot.Collections.Array<SpatialMaterial> mats = 
        new Godot.Collections.Array<SpatialMaterial>();
    enum specialfacetype {Selected,Player,Hover}

    public void OnRoomHover(Godot.Collections.Array properties){
        int selectedface = ((Godot.Collections.Array) sphere!.facecolliders).IndexOf(properties[0]);
        
        specialfaces[(int) specialfacetype.Hover] = new Godot.Collections.Array{selectedface};
        ChangeTextures();
    }

    public void PlayerRoomHighlight() {
        GameState _state = GetNode<GameState>("/root/GameState");
        int selectedface = _state.CurrentPlayerRoom;
        
        specialfaces[(int) specialfacetype.Player] = new Godot.Collections.Array{selectedface};
        ChangeTextures();
    }

    public void SelectionHighlight(Godot.Collections.Array arr) {
        specialfaces[(int) specialfacetype.Selected] = arr;
        ChangeTextures();
    }
    
    private void ChangeTextures() {

        for (int i = 0; i < sphere!._array_mesh.GetSurfaceCount(); i++)
            sphere._array_mesh.SurfaceSetMaterial(i, mats[sizeof(specialfacetype)]);

        for (int i = 0; i < sizeof(specialfacetype); i++) {
            if (specialfaces[i] != null) {
                for (int j = 0; j < ((Godot.Collections.Array) specialfaces[i]).Count; j++) {
                    sphere._array_mesh.SurfaceSetMaterial( ( (int) ((Godot.Collections.Array)specialfaces[i])[j]), mats[i]);
                }
            }
        }


    }


    public override void _Ready() {
        this.AddChild(new PathSelector());
        sphere = GetNode<IcoSphere>("/root/Icosphere/Sphere");

        specialfaces.Resize(sizeof(specialfacetype));
        mats.Resize(sizeof(specialfacetype)+1);

        InitMaterials();
        PlayerRoomHighlight();

    }


    private void InitMaterials () {
        SpatialMaterial hovermat = new SpatialMaterial();
        hovermat.AlbedoColor = new Color(1,1,1,1);
        mats[(int) specialfacetype.Hover] = hovermat;

        SpatialMaterial playerposmaterial = new SpatialMaterial();
        playerposmaterial.AlbedoColor = new Color(1,0.5f,1,1);
        mats[(int) specialfacetype.Player] = playerposmaterial;

        SpatialMaterial selectedmaterial = new SpatialMaterial();
        selectedmaterial.AlbedoColor = new Color(0,1,0.9f,0.4f);
        // one higher than resize, bc 0 indexed
        mats[(int) specialfacetype.Selected] = selectedmaterial;



        SpatialMaterial defaultmaterial = new SpatialMaterial();
        defaultmaterial.AlbedoColor = new Color(0.4f,0.1f,0.7f,0.4f);
        // one higher than resize, bc 0 indexed
        mats[sizeof(specialfacetype)] = defaultmaterial;
    }

    IcoSphere? sphere;
}