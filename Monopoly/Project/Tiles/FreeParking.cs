namespace Monopoly;

// CORNER TILE
public class FreeParking : Tile
{
	public FreeParking(int position, string? tileName, string? tileDescription)
	{
		_tileType = TileType.FREE_PARKING;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
	}
}