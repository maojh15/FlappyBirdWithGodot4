using Godot;
using System;

public partial class MainCamera : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var tubeStartPos = GetNode<Marker2D>("spawnPosition").Position;
		var tubeBottomPos = GetNode<Marker2D>("spawnPositionBottom").Position;
		var tubeSpawner = GetNode<TubeSpawner>("TubeSpawner");
		tubeSpawner.SpawnPosition = tubeStartPos;
		tubeSpawner.SpawnBottomPostion = tubeBottomPos;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
