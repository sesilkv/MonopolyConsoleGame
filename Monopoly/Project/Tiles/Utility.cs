namespace Monopoly;

// UTILITY: electric company & water works
public class Utility : Tile
{
	public Utility(int position, string? tileName, string? tileDescription)
	{
		_tileType = TileType.UTILITY;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
	}
}