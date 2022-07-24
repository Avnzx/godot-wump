using Godot;

class ReasonForEnd : TextureRect {

    public override void _Ready(){

        StreamTexture? stex;

        switch (GetNode<GameState>("/root/GameState").endReason){

            case SceneManager.EndReason.Wumpus:
                stex = (StreamTexture) ResourceLoader.Load("res://assets/fonts/wumpus_death.png");
            break;

            case SceneManager.EndReason.Pit:
                stex = (StreamTexture) ResourceLoader.Load("res://assets/fonts/pit_die.png");
            break;

            case SceneManager.EndReason.Win:
                stex = (StreamTexture) ResourceLoader.Load("res://assets/fonts/arrow_win.png");
            break;

            case SceneManager.EndReason.Arrows:
                stex = (StreamTexture) ResourceLoader.Load("res://assets/fonts/arrow_die.png");
            break;

            default:
                throw new System.ArgumentOutOfRangeException("no end reason specified");
        }
        
        // StreamTexture stex = (StreamTexture) ResourceLoader.Load("res://assets/fonts/arrow_win.png");
        
        GD.Print(stex.GetType());
        this.Texture = stex;
    }

}