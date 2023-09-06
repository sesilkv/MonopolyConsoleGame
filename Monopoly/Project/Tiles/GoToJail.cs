namespace Monopoly;

// CORNER TILE
// get out of jail: roll doubles, get oute free card, pay the fine $50
public class GoToJail : Tile
{
	public GoToJail(int position, string? tileName, string? tileDescription)
	{
		_tileType = TileType.GO_TO_JAIL;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
	}
}