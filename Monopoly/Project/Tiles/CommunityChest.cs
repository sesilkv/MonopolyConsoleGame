namespace Monopoly;

public class CommunityChest : Tile
{
	public CommunityChest(int position, string? tileName, string? tileDescription)
	{
		_tileType = TileType.CommunityChest;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
	}
}