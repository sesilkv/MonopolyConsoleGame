namespace Monopoly;

// CORNER TILE
public class VisitingJail : Tile
{
	public VisitingJail(int position, string? tileName, string? tileDescription)
	{
		_tileType = TileType.VisitingJail;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
	}
}