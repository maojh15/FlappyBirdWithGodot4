using Godot;
using System;
using Label = System.Reflection.Emit.Label;

public partial class tube : Node2D
{
	[Export] public float Speed = 100.0f; // speed of tube in world
	[Export] public float DistanceBetweenTube = 80.0f; // distance between up-tube and down-tube.

	[Signal]
	public delegate void PlayerGetScoreEventHandler();
	
	private Vector2 _velocity;
	private StaticBody2D _upTube, _downTube;
	private Area2D _getScoreArea;
	private TextureRect _texture;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_velocity = Vector2.Left * Speed;
		_upTube = GetNode<StaticBody2D>("up_tube");
		_downTube = GetNode<StaticBody2D>("down_tube");
		_getScoreArea = GetNode<Area2D>("GetScore");
		_InitTubePosition();
		_texture = GetNode<TextureRect>("up_tube/TextureRect");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += _velocity * (float)delta;
	}

	private void _InitTubePosition()
	{
		_upTube.Position = Vector2.Zero;
		var pos = _downTube.Position;
		pos.Y += DistanceBetweenTube;
		_downTube.Position = pos;
	}

	private void _on_visible_on_screen_enabler_2d_screen_exited()
	{
		GD.Print("free tube");
		QueueFree();
	}

	private void EmitHitTube(Node2D body)
	{
		GD.Print("Hit body");
		if (body is player)
		{
			GD.Print("Emit hitTube");
			EmitSignal("HitTube");
		}
	}

	private void EmitGetScore(Node2D body)
	{
		if (body is player)
		{
			GD.Print("Get score");
			EmitSignal("PlayerGetScore");
		}
	}

	public void DisableProcess()
	{
		SetProcess(false);
	}
}
