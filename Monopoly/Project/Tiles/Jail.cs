namespace Monopoly;

// CORNER TILE
// get out of jail: roll doubles, get oute free card, pay the fine $50
public class Jail : Tile
{
	private int _payJail;
	public int PayJail { get { return _payJail; } }
	public Jail(int position, string? tileName, string? tileDescription, int payJail)
	{
		_tileType = TileType.GoToJail;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
		_payJail = payJail;
	}
}