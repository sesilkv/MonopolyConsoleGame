namespace Monopoly;

// TAX
public class Tax : Tile
{
	private int _payTax;
	public Tax(int position, string? tileName, string? tileDescription, int payTax)
	{
		_tileType = TileType.Tax;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
		_payTax = payTax;
	}

	public int PayTax { get => _payTax; set => _payTax = value; }
}