using Godot;
using System;

public partial class TubeSpawner : Node2D
{
	[Export] public PackedScene TubeSceneSpawner;
	
	[Signal] public delegate void UpdateScoreEventHandler();

	public Vector2 SpawnPosition, SpawnBottomPostion;
	private Timer _spawnTimer;
	private Timer _firstSpawnTimer;
	private Random _randGenerator;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetProcess(false);
		_spawnTimer = GetNode<Timer>("spawnTimer");
		_spawnTimer.Stop();
		_firstSpawnTimer = GetNode<Timer>("firstSpawnTimer");
		_randGenerator = new Random(DateTime.Now.Millisecond);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void StartGame()
	{
		SetProcess(true);
		_firstSpawnTimer.Start();
	}

	private void StartTubeGenerator()
	{
		GenerateNewTube();
		_spawnTimer.Start();
	}
	
	private void GenerateNewTube()
	{
		tube newTube = TubeSceneSpawner.Instantiate<tube>();
		var pos = SpawnPosition;
		pos.X += newTube.GetNode<TextureRect>("up_tube/TextureRect").Size.X/2;
		pos.Y = (float)_randGenerator.NextDouble() *
			(SpawnBottomPostion.Y - SpawnPosition.Y - newTube.DistanceBetweenTube) + SpawnPosition.Y;
		newTube.Position = pos;
		newTube.PlayerGetScore += EmitUpdateScore;
		AddChild(newTube);
		GD.Print($"Tube Generated At {newTube.Position}");
	}

	private void EmitUpdateScore()
	{
		EmitSignal("UpdateScore");
	}

	private void OnGameOver()
	{
		_spawnTimer.Stop();
	}
}
