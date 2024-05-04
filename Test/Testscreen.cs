using Godot;
using System;

public partial class Testscreen : Node2D
{
	private PackedScene cardPrefab;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cardPrefab = ResourceLoader.Load("res://Card/Cardcontroller.tscn") as PackedScene;
		InstantiateCard();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
private void InstantiateCard()
{	
	GD.Print("Instantiate card");




	for (int j = 1; j <= 13; j++)
    	{
			for (int i = 0; i < 5; i++)
			{
			Cardcontroller instantiatecard = cardPrefab.Instantiate<Control>() as Cardcontroller;
			instantiatecard.Position =  new Vector2(300, 540);
			instantiatecard.OnCardInstantiate(i,j);
			AddChild(instantiatecard);


			var scene = GD.Load<PackedScene>("res://Card/Cardcontroller.tscn");
			var instance = scene.Instantiate<Cardcontroller>();
			instance.Position = new Vector2(300, 540);
			instance.OnCardInstantiate(i,j);
			AddChild(instance);
				}

			}
        }
    }

