namespace Monopoly;

public class Property : Tile
{
	private PropertyType _propType;
	private int _buyProp;
	private int _rentProp;
	private int _housePrice;
	private string? _ownerProp;
	private int _numberOfHouse;
	private PropertySituation _propSituation;
	public Property(int position, string? tileName, string? tileDescription, int buyProp, int housePrice, int rentProp, PropertyType propType) 
	{
		// pay rent dynamic
		_tileType = TileType.Property;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
		_propType = propType;
		_buyProp = buyProp;
		_rentProp = rentProp;
		_housePrice = housePrice;
		_numberOfHouse = 0;
		_ownerProp = null;
		_propSituation = PropertySituation.Unowned;
	}
	
	// for airport, safe space
	public Property(int position, string? tileName, string? tileDescription, PropertyType propType)
	{
		_tileType = TileType.Property;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
		_propType = propType;
	}

	public PropertyType PropType { get => _propType; set => _propType = value; }
	public int BuyProp { get => _buyProp; set => _buyProp = value; }
	public int RentProp 
	{
		get { return _rentProp; }
		set { _rentProp = value; }
	}
	public PropertySituation PropSituation { get => _propSituation; set => _propSituation = value; }
	public int NumberOfHouse { get => _numberOfHouse; }
	public int HousePrice 
	{ 
		get => _housePrice; 
	}
	
	public void AddHouse()
	{
		_numberOfHouse++;
	}
	
	public void RemoveHouse()
	{
		_numberOfHouse--;
	}
	
	public string Owner { get => _ownerProp; }
	
	public void SetOwner(string owner)
	{
		_ownerProp = owner;
		_propSituation = PropertySituation.Owned;
	}
		
	
}