using Godot;
using System;

public partial class ground : Node2D
{
	[Export] public float slideSpeed = 100;

	private Sprite2D groundPart1, groundPart2;
	private Vector2 velocity;
	private double wrapPositionX; // where swap two ground parts.

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		groundPart1 = GetNode<Sprite2D>("ground_part1");
		groundPart2 = GetNode<Sprite2D>("ground_part2");
		velocity = new Vector2(slideSpeed, 0);
		wrapPositionX = Math.Min(groundPart1.Position.X, groundPart2.Position.X);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		groundPart1.Position -= velocity * (float)delta;
		groundPart2.Position -= velocity * (float)delta;

		if (groundPart1.Position.X < groundPart2.Position.X &&
		    groundPart2.Position.X < wrapPositionX)
		{
			Vector2 newPos = groundPart1.Position;
			newPos.X = groundPart2.Position.X + groundPart1.Texture.GetWidth();
			groundPart1.Position = newPos;
		}
		else if (groundPart1.Position.X < wrapPositionX)
		{
			Vector2 newPos = groundPart2.Position;
			newPos.X = groundPart1.Position.X + groundPart1.Texture.GetWidth();
			groundPart2.Position = newPos;
		}
	}
}
