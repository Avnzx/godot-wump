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
        GD.Print(ValidateSelection(selectedface));

        if (ValidateSelection(selectedface))
            selectedfaces.Add(selectedface);

                
        _selector!.SelectionHighlight(selectedfaces);
    }

    public bool ValidateSelection(int selection) {
        GameState _state = GetNode<GameState>("/root/GameState");
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