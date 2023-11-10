using Godot;
using System;

public partial class player : CharacterBody2D
{
	// public const float Speed = 300.0f;
	[Export] public float JumpVelocity = 400.0f;
	[Export] public float costumGravity = 980.0f;

	[Signal]
	public delegate void GameStartEventHandler();

	[Signal]
	public delegate void GameOverEventHandler();

	private bool _gameOverEmited = false;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private AnimationNodeStateMachinePlayback _animationStateMachien;
	private bool _gamePlaying = false;

	public override void _Ready()
	{
		base._Ready();
		GetNode<AnimationTree>("AnimationTree").Active = true;
		_animationStateMachien = (AnimationNodeStateMachinePlayback)GetNode<AnimationTree>("AnimationTree").Get("parameters/playback");
		Velocity = Vector2.Zero;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	public override void _UnhandledKeyInput(InputEvent @event)
	{
		base._UnhandledKeyInput(@event);
		if (@event.IsActionPressed("jump"))
		{
			_animationStateMachien.Start("fly_up", true);
			Velocity = Vector2.Up * JumpVelocity;
			if (!_gamePlaying)
			{
				_gamePlaying = true;
				EmitSignal("GameStart");
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!_gamePlaying)
		{
			return;
		}

		Velocity += Vector2.Down * costumGravity * (float)delta;
		
		MoveAndSlide();

		if (!_gameOverEmited)
		{
			for (int i = 0; i < GetSlideCollisionCount(); ++i)
			{
				var collision = GetSlideCollision(i);
				var collider = collision.GetCollider();
				if (collider is StaticBody2D body)
				{
					var parent = body.GetParent();
					if (parent is ground)
					{
						GD.Print("Collide with ground");
						EmitSignal("GameOver");
						_gameOverEmited = true;
					} else if (parent is tube)
					{
						GD.Print("Collide with tube");
						EmitSignal("GameOver");
						_gameOverEmited = true;
					}
				}
			}
		}
	}
}
