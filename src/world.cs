using Godot;
using System;

public partial class world : Node
{
	private HUD _hud;

	private ground _ground;

	private player _player;

	private Label _restart_hint, _jump_hint;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_hud = GetNode<HUD>("HUD");
		_ground = GetNode<ground>("Ground");
		_player = GetNode<player>("Player");
		_restart_hint = GetNode<Label>("HUD/RestartHint");
		_jump_hint = GetNode<Label>("HUD/HintJump");
		GetNode<ColorRect>("GameOverFlash").Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _UnhandledKeyInput(InputEvent @event)
	{
		base._UnhandledKeyInput(@event);
		if (_restart_hint.Visible && @event.IsActionPressed("jump"))
		{
			RestartGame();
		}

		if (_jump_hint.Visible && @event.IsActionPressed("jump"))
		{
			_jump_hint.Hide();
		}
	}

	public void RestartGame()
	{
		GetTree().CallGroup("tube_group", "QueueFree");
		var reload_result = GetTree().ReloadCurrentScene();
		if (reload_result != Error.Ok)
		{
			GD.Print("Fail for reloading scene");
		}
	}

	public void UpdateScore()
	{
		_hud.AddScore();
	}

	public async void OnGameOver()
	{
		GD.Print("Game over");
		
		GetTree().CallGroup("tube_group", "DisableProcess");
		_ground.SetProcess(false);
		_player.SetProcessUnhandledKeyInput(false);	
		GetNode<AnimationPlayer>("FlashAnimate").Play("flashAnimation");
		_hud.ShowGameOver();
		await ToSignal(GetTree().CreateTimer(0.8), SceneTreeTimer.SignalName.Timeout);
		_restart_hint.Visible = true;
	}
}
