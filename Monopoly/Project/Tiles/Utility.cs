namespace Monopoly;

// UTILITY: electric company & water works
public class Utility : Tile
{
	private int _payUtility;
	public Utility(int position, string? tileName, string? tileDescription, int payUtility)
	{
		_tileType = TileType.Utility;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
		_payUtility = payUtility;
	}
	
	public int PayUtility { get => _payUtility; set => _payUtility = value; }
}