namespace Monopoly;

public class Chance : Tile
{
	public Chance(int position, string? tileName)
	{
		_tileType = TileType.Chance;
		_tileName = tileName;
		_position = position;
	}
	
}