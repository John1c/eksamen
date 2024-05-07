using Godot;
using System;
using System.Collections.Generic;

public partial class Cardcontroller : Node2D
{
		private bool isDragging = false;
	    private int _cardID;
        private int _CardPattern;
		private bool _faceUp = true;
	    private Label _CardLabel;

    public int cardID {
		get{ return _cardID; }
		set{ _cardID = value; }
	}

    public int CardPattern {
		get{ return _CardPattern; }
		set{ _CardPattern = value; }
	}

   public bool faceUp {
		get{ return _faceUp; }
		set{ _faceUp = value; }
	}

public void OnCardInstantiate(int ID, int Pattern){
        cardID = ID;
        CardPattern = Pattern;
        _CardLabel = GetNode<Label>("Control/CardLabel");
        _CardLabel.Text = "ID: "+ ID.ToString() +"\n"+ "Pattern: " + Pattern.ToString(); 
    }
// Tjekker om kortet er faceup eller facedown.
public void UpdateCard()
	{
		if(faceUp == true)
		{
        	_CardLabel.Show();
		} 
		if(faceUp == false)
		{
			_CardLabel.Hide();
		}
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
