using Villa_API.Models.Dto;

namespace Villa_API.Data
{
	public static class VillaStore
	{
		public static List<VillaDTO> villalist = new List<VillaDTO>
			{
				new VillaDTO {Id=1,Name="pool Villa",Occupancy=3,Sqft=200 },
				new VillaDTO {Id=2,Name="beach Villa",Occupancy=5,Sqft=400 }
			};
	}
}
