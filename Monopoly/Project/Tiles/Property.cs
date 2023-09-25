namespace Monopoly;

public class Property : Tile
{
	private PropertyType _propType;
	private int _buyProp;
	private int _rentProp;
	private int _housePrice;
	private int _hotelPrice;
	private string? _ownerProp;
	private int _numberOfHouse;
	private int _numberOfHotel;
	private PropertySituation _propSituation;
	public Property(int position, string? tileName, string? tileDescription, int buyProp, int housePrice, int hotelPrice, int rentProp, PropertyType propType) 
	{
		// pay rent dynamic
		_tileType = TileType.Property;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
		_propType = propType;
		_buyProp = buyProp;
		_hotelPrice = hotelPrice;
		_rentProp = rentProp;
		_housePrice = housePrice;
		_numberOfHouse = 0;
		_numberOfHotel = 0;
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

    public Property(int position, string? tileName, string v1, int v2, int v3, int v4, PropertyType country) : base(position, tileName)
    {
    }

    public PropertyType PropType { get => _propType; set => _propType = value; }
	public int BuyProp { get => _buyProp; set => _buyProp = value; }
	public int RentProp { get => _rentProp; set => _rentProp = value; }
	public PropertySituation PropSituation { get => _propSituation; set => _propSituation = value; }
	public int NumberOfHouse { get => _numberOfHouse; }
	public int HousePrice { get => _housePrice; }
	public int HotelPrice { get => _hotelPrice; }
	public int NumberOfHotel { get => _numberOfHotel; }
	
	public void AddHouse()
	{
		_numberOfHouse++;
	}
	
	public void AddHotel()
	{
		_numberOfHotel++;
	}
	
	public void RemoveHouse()
	{
		_numberOfHouse--;
	}
	
	public void RemoveHotel()
	{
		_numberOfHotel--;
	}
	
	public string Owner { get => _ownerProp; }
	
	public void SetOwner(string owner)
	{
		_ownerProp = owner;
		_propSituation = PropertySituation.Owned;
	}
}