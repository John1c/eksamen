using Godot;
using System;

public partial class Testscreen : Node2D
{
	private PackedScene cardPrefab;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cardPrefab = ResourceLoader.Load("uid://cfufscq00nrbm") as PackedScene;
		InstantiateCard();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
private void InstantiateCard()
{	
	GD.Print("Instantiate card");

	for (int i = 1; i < 5; i++)
	{
		for (int j = 1; j <= 13; j++)
    	{
				GD.Print("j="+ j +" I=" + i);
			Cardcontroller instantiatecard = cardPrefab.Instantiate<Node2D>() as Cardcontroller;
			instantiatecard.Position =  new Vector2(i*100, j*10);
			instantiatecard.OnCardInstantiate(j,i);
			AddChild(instantiatecard);

			}
        }
	}
}

