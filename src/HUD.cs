using Godot;
using System;

public partial class HUD : Control
{
	private Label _scoreLabel;
	private int _score = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_scoreLabel = GetNode<Label>("Score");
		_scoreLabel.Text = _score.ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void AddScore()
	{
		_score += 1;
		_scoreLabel.Text = _score.ToString();
	}
	
	public void ShowGameOver()
	{
		GetNode<TextureRect>("GameOver").Visible = true;
	}
}
