namespace Monopoly;

// CORNER TILE
public class Go : Tile
{
	private int _amountStart;
	public int AmountStart { get { return _amountStart; } }
	public Go(int position, string? tileName, string? tileDescription, int amountStart)
	{
		_tileType = TileType.Go;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
		_amountStart = amountStart;
	}
}