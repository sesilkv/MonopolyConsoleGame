namespace Monopoly;

// CORNER TILE
public class VisitingJail : Tile
{
	public VisitingJail(int position, string? tileName, string? tileDescription)
	{
		_tileType = TileType.VISITING_JAIL;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
	}
}