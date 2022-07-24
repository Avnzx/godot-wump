using Godot;

class Shooter : CanvasLayer {

    public Shooter() {
        this.Name = "Shooting layer";
    }

    public void HandleButtonSignal(Godot.Collections.Array arr) {
        CustButton.ShooterButtonAction action = (CustButton.ShooterButtonAction) arr[0];

        if(action == CustButton.ShooterButtonAction.Shoot) {
            Shoot();
        } else {
            ClearShoot();
        }
        
    }

    public void Shoot() {
        if (GetParent<PathSelector>().selectedfaces.Contains(GetNode<GameState>("/root/GameState").CurrentMonsterRoom)) {
            GetNode<SceneManager>("/root/SceneManager").GotoEndScene(SceneManager.EndReason.Win);
        } else {
            GetNode<GameState>("/root/GameState").NumArrows -= 1;
        }
    }
    public void ClearShoot() {
        // clear the shooter array and free itself to remove the overlay
        GD.Print("tried freeing");
        GetParent<PathSelector>().selectedfaces = new Godot.Collections.Array();
        GetParent<PathSelector>().shooter = null;
        GetParent<PathSelector>().UpdateSelection();

        this.QueueFree();
    }
    

    [Signal]
    public delegate void Clear();

}