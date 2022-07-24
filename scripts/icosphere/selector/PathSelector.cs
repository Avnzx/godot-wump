using Godot; 

class PathSelector : Node {
    GameState? _state;
    FaceSelect? _selector;
    IcoSphere? sphere;

    public Godot.Collections.Array selectedfaces = new Godot.Collections.Array();

    public override void _Ready() {
        sphere = GetNode<IcoSphere>("/root/Icosphere/Sphere");
        this.Name = "pathselector";
        _state = GetNode<GameState>("/root/GameState");
        _selector = GetNode<FaceSelect>("/root/Icosphere/faceselector");

        selectedfaces.Resize(1);
    }

    public void HandleNewSelectionAttempt(Godot.Collections.Array properties) {
        int selectedface = ((Godot.Collections.Array) sphere!.facecolliders).IndexOf(properties[0]);


        if (ValidateSelection(selectedface)) {
            selectedfaces.Add(selectedface);
            AddShooter();
        }
 
                
        UpdateSelection();
    }

    public void UpdateSelection() {
        _selector!.SelectionHighlight(selectedfaces);
    }

    public Shooter? shooter;

    private void AddShooter() {
        if (shooter == null) {
            GD.Print("shooter is null");
        } else {
            GD.Print("shooter is not null");
        }

        if(selectedfaces.Count == 2 && shooter == null) {
            shooter = ResourceLoader.Load<PackedScene>("res://scenes/overlays/shootingoverlay.tscn").Instance<Shooter>();
            AddChild(shooter,true);
        }    
    }

    public bool ValidateSelection(int selection) {
        GameState _state = GetNode<GameState>("/root/GameState");
        GD.Print("selected faces?  ", selectedfaces.Count);
        if (selectedfaces.Count == 0) {
            selectedfaces.Resize(1);
        }
        selectedfaces[0] = _state.CurrentPlayerRoom;
        

        if (
            ((Godot.Collections.Array)_state!.adjacency![selection]).Contains(selectedfaces[selectedfaces.Count-1]) 
            && !selectedfaces.Contains(selection) 
            && (selectedfaces.Count != GameConstants.max_shooter_dist+1))
        {
            return true;
        } else {
            return false;
        }
    }
}