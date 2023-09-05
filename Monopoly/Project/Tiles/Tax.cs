namespace Monopoly;

// TAX
public class Tax : Tile
{
	private int _paymentTax;
	public Tax(int position, string? tileName, string? tileDescription, TileType tileType, int paymentTax) : base(position, tileName, tileDescription, tileType)
	{
		_paymentTax = paymentTax;
	}

	public int PaymentTax { get => _paymentTax; set => _paymentTax = value; }
}