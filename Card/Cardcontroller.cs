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
	public bool IsStacked_on_deck = false;
	public bool Has_card_stacked = false;
	public bool is_at_start = false;

	public Cardcontroller Stacked_on_card;
	public Cardcontroller Stacked_on_finish_card;
	public Testscreen Stacked_on_finish_dropzone;
	public Control Stacked_on_finish_area;
	public Control Stacked_on_Deck_area;
	public Vector2 Prev_pos;

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

	public bool faceUp
	{
		get { return _faceUp; }
		set { _faceUp = value; }
	}
	public void OnCardInstantiate(int ID, int Pattern)
	{
		cardID = ID;
		CardPattern = Pattern;
		_CardLabel = GetNode<Label>("Control/CardLabel");
		_CardLabel.Text = "ID: " + ID.ToString() + "\n" + "Pattern: " + Pattern.ToString();
	}

	// Tjekker om kortet er faceup eller facedown.
	public void UpdateCard()
	{
		if (faceUp)
		{
			_CardLabel.Show();

		}
		if (!faceUp)
		{
			_CardLabel.Hide();

		}
	}
	private Vector2 offset = new(0, 0);
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
	}

	public void move_to_front()
	{
		GetParent().MoveChild(this, GetParent().GetChildCount());
	}

	public void OnMouseDown() //
	{
	if(faceUp){
	 move_to_front();
	 IsStacked = false;
	 IsStacked_on_finish = false;
	 IsStacked_on_deck = false;
	 isDragging = true;
	 Prev_pos = Position;
	 }
	 if(Stacked_on_card.Has_card_stacked){ // Hvis kort bliver flyttet bliver det kort over markeret, så den ved at den er gået
		 Stacked_on_card.Has_card_stacked = false;
	 }
	}

	public void OnMouseUp()
	{
		isDragging = false;
		if (!IsStacked && !IsStacked_on_finish && !IsStacked_on_deck && faceUp)
		{
			Position = Prev_pos;
		}
		
	}

	public void _on_area_2d_area_entered(Area2D area)
	{
		Cardcontroller card = area.GetParent() as Cardcontroller;
		Control dropzone = area.GetParent() as Control;



		if (area.GetParent().Name == CardPattern.ToString())
		{
			Stack_on_finish_dropzone(area, dropzone);
		}
		if (area.GetParent().GetParent().Name == "DeckKort")
		{
			Stack_on_Deck(area, dropzone);
		}
			if (!IsStacked && !is_at_start) Stack_on_card(area, card);
			if (card.IsStacked_on_finish) Stack_on_finish_card(area, card);
	}
	public void Stack_on_card(Area2D area, Cardcontroller card)
	{
		Stacked_on_card = card;

		if (!card.Has_card_stacked && isDragging && card.faceUp && cardID == card.cardID - 1 && ((CardPattern % 2 == 0 && card.CardPattern % 2 != 0) || (CardPattern % 2 != 0 && card.CardPattern % 2 == 0)))
		{
			// Check if the card being stacked on has a lower ID and the patterns have opposite parity
			if (!IsStacked)
			{
				isDragging = false;
				card.Has_card_stacked = true;
				Position = card.Position + new Godot.Vector2(0, 60);
				IsStacked = true;
			}
			//Movecount++;
		}
	}
	public void Stack_on_finish_card(Area2D area, Cardcontroller card)
	{
		if (isDragging && cardID == card.cardID + 1 && CardPattern == card.CardPattern)
		{
			isDragging = false;
			IsStacked_on_finish = true;
			Stacked_on_finish_card = card;
			ZIndex = Stacked_on_finish_card.ZIndex + 1;
			Position = Stacked_on_finish_card.Position;
		}
	}
	public void Stack_on_finish_dropzone(Area2D area, Control card)
	{
		if (isDragging && cardID == 1)
		{
			Stacked_on_finish_area = card;
			isDragging = false;
			IsStacked = false;
			IsStacked_on_finish = true;
			ZIndex = Stacked_on_finish_area.ZIndex + 1;
			Position = Stacked_on_finish_area.Position;
		}
	}
	public void Stack_on_Deck(Area2D area, Control card)
	{

		GD.Print(card.Name);
		if (isDragging && cardID == 13)
		{
			Stacked_on_Deck_area = card;
			isDragging = false;
			IsStacked = false;
			IsStacked_on_deck = true;
			ZIndex = Stacked_on_Deck_area.ZIndex + 1;
			Position = Stacked_on_Deck_area.Position;
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
			ZIndex = Stacked_on_card.ZIndex + 1;
			Position = Stacked_on_card.Position + new Godot.Vector2(0, 60);
		}
	}
}
