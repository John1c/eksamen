using Godot;
using System;
using System.Collections.Generic;

public partial class Cardcontroller : Control
{
		private bool isDragging = false;
	    private int _cardID;
        private int _CardPattern;
	    private Label _CardLabel;

    public int cardID {
		get{ return _cardID; }
		set{ _cardID = value; }
	}

    public int CardPattern {
		get{ return _CardPattern; }
		set{ _CardPattern = value; }
	}

public void OnCardInstantiate(int cardID, int CardPattern){
        _cardID = cardID;
        _CardPattern = CardPattern;
        _CardLabel = GetNode<Label>("CardLabel");
        _CardLabel.Text = "ID: "+ cardID.ToString() +"\n"+ "Pattern: " + CardPattern.ToString();
    }

	private Vector2 offset = new(0,0);
    public override void _Input(InputEvent @event)
	{
	// Mouse in viewport coordinates.
		if (@event is InputEventMouseMotion eventMouseMotion){
			if(isDragging){
		 MoveWithMouse(eventMouseMotion.Position + offset);
		}else{
			offset = Position - eventMouseMotion.Position;
			}
		} 
	}

    private void MoveWithMouse(Vector2 v)
    {
        Position = v;
    }
    
    public void OnMouseDown() {
		isDragging = true;
	}

	public void OnMouseUp(){
		isDragging = false;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
      
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
