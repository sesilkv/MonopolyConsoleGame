namespace Monopoly;

// TAX
public class Tax : Tile
{
	private int _paymentTax;
	public Tax(int position, string? tileName, string? tileDescription, int paymentTax)
	{
		_tileType = TileType.TAX;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
		_paymentTax = paymentTax;
	}

	public int PaymentTax { get => _paymentTax; set => _paymentTax = value; }
}