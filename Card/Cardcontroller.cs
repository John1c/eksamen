using Godot;
using System;
using System.Collections.Generic;

public partial class Cardcontroller : Node2D
{
	
	
  	private bool _faceUp = true;
	private bool isDragging = false;
	private int _cardID;
	private int _CardPattern;
	private Label _CardLabel;

	public bool IsStacked = false;
	public bool IsStacked_on_finish = false;

	public Cardcontroller Stacked_on_card;
	public Cardcontroller Stacked_on_finish_card;
	public Testscreen Stacked_on_finish_dropzone;
	public Control Stacked_on_finish_area;

	public int cardID
	{
		get { return _cardID; }
		set { _cardID = value; }
	}

	public int CardPattern
	{
		get { return _CardPattern; }
		set { _CardPattern = value; }
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
		if(faceUp)
		{
        	_CardLabel.Show();

		} 
		if(!faceUp)
		{
			_CardLabel.Hide();
			
		}
	}
      private Vector2 offset = new(0,0);
      public override void _Input(InputEvent @event)
	{
		// Mouse in viewport coordinates.
		if (@event is InputEventMouseMotion eventMouseMotion)
		{
			if (isDragging)
			{
				MoveWithMouse(eventMouseMotion.Position + offset);
			}
			else
			{
				offset = Position - eventMouseMotion.Position;
			}
		}
	}

			private void MoveWithMouse(Godot.Vector2 v)
	{
		Position = v;
		IsStacked = false;
	}

	public void move_to_front()
	{
		GetParent().MoveChild(this, GetParent().GetChildCount());
	}

	public void OnMouseDown()
	{
    if(faceUp){
	isDragging = true;
	}
	}

	public void OnMouseUp()
	{
		isDragging = false;
	}

	public void _on_area_2d_area_entered(Area2D area)
	{
		if(faceUp)
		{
		if(area.GetParent().Name == CardPattern.ToString())
		{
		Control dropzone = area.GetParent() as Control;
		Stack_on_finish_dropzone(area, dropzone);
		}
		if(area.GetParent().Name == CardPattern.ToString())
		{
		Control dropzone = area.GetParent() as Control;
		Stack_on_finish_dropzone(area, dropzone);
		}
		else
		{
		Cardcontroller card = area.GetParent() as Cardcontroller;
		if(!IsStacked)Stack_on_card(area, card);
		if(card.IsStacked_on_finish)Stack_on_finish_card(area, card);
		}
	}
	}
	public void Stack_on_card(Area2D area, Cardcontroller card)
	{
		Stacked_on_card = card;

		if (isDragging && cardID == card.cardID - 1 && ((CardPattern % 2 == 0 && card.CardPattern % 2 != 0) || (CardPattern % 2 != 0 && card.CardPattern % 2 == 0)))
		{
			// Check if the card being stacked on has a lower ID and the patterns have opposite parity
			move_to_front();
			if (!IsStacked)
			{
				isDragging = false;
				Position = card.Position + new Godot.Vector2(0, 60);
				IsStacked = true;
			}
			//Movecount++;
		}
	}
	public void Stack_on_finish_card(Area2D area, Cardcontroller card){
		if(isDragging && cardID == card.cardID+1 && CardPattern == card.CardPattern){
		isDragging = false;
		IsStacked_on_finish = true;
		Stacked_on_finish_card = card;
		ZIndex = Stacked_on_finish_card.ZIndex + 1;
		Position = Stacked_on_finish_card.Position;
		}
	}
	public void Stack_on_finish_dropzone(Area2D area, Control card){
		if(isDragging && cardID == 1)
		{
		Stacked_on_finish_area = card;
		isDragging = false;
		IsStacked_on_finish = true;
		ZIndex = Stacked_on_finish_area.ZIndex + 1;
		Position = Stacked_on_finish_area.Position;
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (IsStacked)
		{
			//Move the card to the position of the card it is stacked on
			move_to_front();
			ZIndex = Stacked_on_card.ZIndex + 1;
			Position = Stacked_on_card.Position + new Godot.Vector2(0, 60);
		}
	}
}
