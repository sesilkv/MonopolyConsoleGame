namespace Monopoly;

public class CommunityChest : Tile
{
	public CommunityChest(int position, string? tileName, string? tileDescription)
	{
		_tileType = TileType.COMMUNITY_CHEST;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
	}
}