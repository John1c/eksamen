using Godot;
using System;
using System.Collections.Generic;
using System.Numerics;

public partial class Cardcontroller : Node2D
{
		private bool isDragging = false;
	    private int _cardID;
        private int _CardPattern;
	    private Label _CardLabel;
		
		public bool IsStacked = false;

    public int cardID {
		get{ return _cardID; }
		set{ _cardID = value; }
	}

    public int CardPattern {
		get{ return _CardPattern; }
		set{ _CardPattern = value; }
	}

public void OnCardInstantiate(int ID, int Pattern){
        cardID = ID;
        CardPattern = Pattern;
        _CardLabel = GetNode<Label>("Control/CardLabel");
        _CardLabel.Text = "ID: "+ ID.ToString() +"\n"+ "Pattern: " + Pattern.ToString();
    }

	private Godot.Vector2 offset = new(0,0);
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

    private void MoveWithMouse(Godot.Vector2 v)
    {
        Position = v;
		IsStacked = false;
    }
    
	public void move_to_front(){
		GetParent().MoveChild(this, GetParent().GetChildCount());
	}

    public void OnMouseDown() {
		isDragging = true;
		move_to_front();
	}

	public void OnMouseUp(){
		isDragging = false;
	}
	public void _on_area_2d_area_entered(Area2D area){
		GD.Print("Area Entered");
		Stack_on_card(area);
	}
	public void Stack_on_card(Area2D area){
		Cardcontroller card = area.GetParent() as Cardcontroller;

		if(isDragging && (cardID+1 == card.cardID && (CardPattern-1 == card.CardPattern || CardPattern+1 == card.CardPattern))){
		//move the card to the top of the stack
		move_to_front();
		//move the card to the position of the card it is stacked on
		isDragging = false;
		IsStacked = true;
        Position = card.Position + new Godot.Vector2(0, 60);
		//Movecount++; senere implementering
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
      
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		/* virker ikke endnu skal lige fået lortet til at com på hindanden
		if(IsStacked){
			//Move the card to the position of the card it is stacked on
			Position = card.Position + new Godot.Vector2(0, 60);
		}
		*/
	}
}
