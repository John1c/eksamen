using Godot;
using System;
using System.Collections.Generic;
public partial class Testscreen : Node2D
{
	List<Cardcontroller> cards = new List<Cardcontroller>();
	private PackedScene cardPrefab;
	List<List<Cardcontroller>> piles = new List<List<Cardcontroller>>();
	List<Cardcontroller> inPlay = new List<Cardcontroller>();
	public List<bool> states = new List<bool>();
	private int buttonPress = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cardPrefab = ResourceLoader.Load("uid://cfufscq00nrbm") as PackedScene;
		InstantiateCard();
		PileMaker();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		update_pile();

		if (inPlay[0].IsStacked || inPlay[0].IsStacked_on_finish || inPlay[0].IsStacked_on_deck)
		{
			inPlay[0].is_at_start = false;
			GD.Print(inPlay.Count);
			inPlay.RemoveAt(0);
			GD.Print(inPlay.Count);
			inPlay[0].faceUp = true;
			inPlay[0].UpdateCard();
		}
	}


	private void InstantiateCard()
	{
		GD.Print("Instantiate card");

		for (int i = 0; i < 4; i++)
		{
			for (int j = 1; j <= 13; j++)
			{
				//	GD.Print("j="+ j +" I=" + i);
				Cardcontroller instantiatecard = cardPrefab.Instantiate<Node2D>() as Cardcontroller;
				instantiatecard.OnCardInstantiate(j, i);

				//			if(i%2==0){
				//				instantiatecard.faceUp = false;
				//			}

				cards.Add(instantiatecard); // Add the card to the list
											//			GD.Print("ID: " + instantiatecard.cardID + " Pattern: " + instantiatecard.CardPattern);
											//			GD.Print(instantiatecard.CardPattern);
			}
		}
		ShuffleDeck();
		for (int i = 0; i < cards.Count; i++)
		{
			cards[i].Position = new Vector2(i * 30, i % 3 * 150);
			AddChild(cards[i]);
			cards[i].UpdateCard();
		}
	}
	public void ShuffleDeck()
	{
		//randomize the deck
		GD.Print("Shuffling deck");
		Random rnd = new Random();
		for (int i = 0; i < cards.Count; i++)
		{
			int r = rnd.Next(cards.Count);
			Cardcontroller temp = cards[i];
			cards[i] = cards[r];
			cards[r] = temp;
		}
	}
	public void PileMaker()
	{
		for (int i = 0; i < 7; i++)
		{
			List<Cardcontroller> tempPile = new List<Cardcontroller>();
			for (int j = 0; j < i + 1; j++)
			{
				tempPile.Add(cards[0]);
				tempPile[j].faceUp = false;
				tempPile[j].UpdateCard();

				//GD.Print(tempPile[j].cardID);	

				//GD.Print(cards[0].cardID);
				cards.Remove(cards[0]);
				//GD.Print(cards[0].cardID.ToString() + "\n");
				tempPile[j].Position = new Vector2(i * 75, j * 110);
				//GD.Print(i);
				//GD.Print(j);
			}
			tempPile.Reverse();
			tempPile[0].faceUp = true;
			tempPile[0].UpdateCard();


			piles.Add(tempPile);
			GD.Print("Cap: " + piles[i].Capacity.ToString());
		}
		for (int i = 0; i < 7; i++)
		{
			bool occupyState = true;
			states.Add(occupyState);
			GD.Print(states[i]);
		}
	}
	public void update_pile()
	{
		for (int i = 0; i < piles.Count; i++)
		{
			if (piles[i].Count == 0)
			{
				states[i] = false;
				GD.Print(states[i]);
			}
			else if (piles[i].Count == 0 || piles[i][0].IsStacked || piles[i][0].IsStacked_on_finish || piles[i][0].IsStacked_on_deck)
			{

				piles[i].RemoveAt(0);
				//		else if(piles[i].Count > 0){
				//
				//		}
				piles[i][0].faceUp = true;
				piles[i][0].UpdateCard();
			}

		}
	}


	private void draw_card()
	{
		buttonPress++;
		//GD.Print(buttonPress);

		//tilføjer kort som kan ses og trækkes ud fra det resterende dæk
		if (cards.Count == 1)
		{
			inPlay.Insert(0, cards[0]);
			cards.Clear();
		}
		else if (cards.Count > 0)
		{
			inPlay.Insert(0, cards[0]);
			cards.RemoveAt(0);
		}
		//Fylder cards op når den er tom
		else if (cards.Count == 0)
		{
			for (int i = 0; i < inPlay.Count; i++)
			{
				cards.Insert(0, inPlay[i]);
				cards[0].faceUp = false;
				cards[0].UpdateCard();
				cards[0].Position = new Vector2(400, 0);
				//				GD.Print(i);
			}
			inPlay.Clear();

		}
		inPlay[0].is_at_start = true;
		inPlay[0].faceUp = true;
		inPlay[0].UpdateCard();
		inPlay[0].move_to_front();
		inPlay[0].Position = new Vector2(300, 200);

		if (inPlay.Count > 1)
		{
			inPlay[1].faceUp = false;
			inPlay[1].UpdateCard();
		}
		GD.Print(inPlay[0].cardID);
		GD.Print(cards.Count);
		GD.Print(inPlay.Count);
	}
}
