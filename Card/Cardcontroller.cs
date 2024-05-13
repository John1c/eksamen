using Godot;
using System;
using System.Collections.Generic;

public partial class Cardcontroller : Node2D
{

	// Globale varibler
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
	public Control Stacked_on_finish_area;
	public Control Stacked_on_Deck_area;
	public Mainscene Deck_area;
	public Vector2 Prev_pos;

	//Get og set metoder
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
	// metode til når kortet bliver instansieret og sætter labelen til at vise kortets ID og mønster.
	//samt at sætte kortets ID og mønster.
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
	// Metode til at bevæge kortet med musen.
	private Vector2 offset = new(0, 0);
	public override void _Input(InputEvent @event)
	{
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
	// Metode til at flytte position med musen.
	private void MoveWithMouse(Godot.Vector2 v)
	{
		Position = v;
	}
	// Metode til at flytte kortet til fronten af scenen.
	public void move_to_front()
	{
		GetParent().MoveChild(this, GetParent().GetChildCount());
	}
	// Metode til når den detecter at man holder musen nede på kortet.
	public void OnMouseDown() //
	{
		if (faceUp)
		{
			move_to_front();
			IsStacked = false;
			IsStacked_on_finish = false;
			IsStacked_on_deck = false;
			isDragging = true;
			Prev_pos = Position;
		}
		if (Stacked_on_card.Has_card_stacked)
		{ // Hvis kort bliver flyttet bliver det kort over markeret, så den ved at den er gået
			Stacked_on_card.Has_card_stacked = false;
		}
	}
	// Metode til når man slipper musen.
	public void OnMouseUp()
	{
		isDragging = false;
		//Hvis kortet ikke er stacked på et andet kort, så rykker det tilbage til sin originale position.
		if (faceUp && !IsStacked && !IsStacked_on_finish && !IsStacked_on_deck)
		{
			Position = Prev_pos;
		}

	}
	// Metode til når kortet er i kontakt med et andet kort.
	public void _on_area_2d_area_entered(Area2D area)
	{
		// Når den detecter et object i area definere den enten et control eller cardcontroller
		Cardcontroller card = area.GetParent() as Cardcontroller;
		Control dropzone = area.GetParent() as Control;


		//tjekker for esser ved navn på control og kort pattern
		if (area.GetParent().Name == CardPattern.ToString())
		{
			Stack_on_finish_dropzone(dropzone);
		}
		//tjekker for navnet af dens parent af parent får at få navnet på det øverste for at se om en konge kan placeres
		if (area.GetParent().GetParent().Name == "DeckKort")
		{
			Stack_on_Deck(dropzone,area.GetParent().Name);
		}
		//tjekker for om kortet er stacked på et andet kort eller om det er ved start eller på et færdigt kort og handler ud fra det
		if (!IsStacked && !card.is_at_start) Stack_on_card(card);
		if (card.IsStacked_on_finish) Stack_on_finish_card(card);
	}
	// Metode til når kortet skal stacke på et andet kort
	public void Stack_on_card(Cardcontroller card)
	{
		Stacked_on_card = card;
		//lang if sætning som tjekker om det kan stack på et kort og om mønstrene er forskellige og om de er lige eller ulige
		if (!card.Has_card_stacked && isDragging && card.faceUp && cardID == card.cardID - 1 && (
			(CardPattern % 2 == 0 && card.CardPattern % 2 != 0) || (CardPattern % 2 != 0 && card.CardPattern % 2 == 0)))
		{
			// forskellige bools bliver sat til true og false og bliver flyttet til kortet som det stacker på
			isDragging = false;
			card.Has_card_stacked = true;
			Position = card.Position + new Godot.Vector2(0, 60);
			IsStacked = true;
			is_at_start = false;
		}
	}
	// Metode til når kortet skal stacke på et færdigt kort
	public void Stack_on_finish_card(Cardcontroller card)
	{
		if (isDragging && cardID == card.cardID + 1 && CardPattern == card.CardPattern)
		{
			//relativt det samme som stack_on_card metoden med få ændringer
			isDragging = false;
			IsStacked_on_finish = true;
			is_at_start = false;
			ZIndex = card.ZIndex + 1;
			Position = card.Position;
		}
	}
	// Metode til når kortet skal stacke på et færdigt zone så kun for esser
	public void Stack_on_finish_dropzone(Control card)
	{
		if (isDragging && cardID == 1)
		{
			//relativt det samme som stack_on_card metoden med få ændringer
			isDragging = false;
			IsStacked = false;
			is_at_start = false;
			IsStacked_on_finish = true;
			ZIndex = card.ZIndex + 1;
			Position = card.Position;
		}
	}
	// Metode til når kortet skal stacke på dækket kun for konger
	public void Stack_on_Deck(Control card, String name)
	{
		Mainscene Deck_area = card.GetParent().GetParent() as Mainscene;
		{
			if (isDragging && cardID == 13)
			{
				//relativt det samme som stack_on_card metoden med få ændringer
				isDragging = false;
				IsStacked = false;
				is_at_start = false;
				IsStacked_on_deck = true;
				ZIndex = card.ZIndex + 1;
				Position = card.Position;

				int nr = name.ToInt();
				int realnr = (nr/10)-1;
				Deck_area.states[realnr] = true;
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Hvis kortet er stacked på et andet kort så flytter det sig til kortet det er stacked på som konstant bliver opdateret
		if (IsStacked)
		{
			//Move the card to the position of the card it is stacked on
			ZIndex = Stacked_on_card.ZIndex + 1;
			Position = Stacked_on_card.Position + new Godot.Vector2(0, 60);
		}
	}
}
