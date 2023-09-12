namespace Monopoly;

public class Chance : Tile
{
	public Chance(int position, string? tileName, string? tileDescription)
	{
		_tileType = TileType.Chance;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
	}
}