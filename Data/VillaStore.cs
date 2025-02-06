using Villa_API.Models.Dto;

namespace Villa_API.Data
{
	public static class VillaStore
	{
		public static List<VillaDTO> villalist = new List<VillaDTO>
			{
				new VillaDTO {Id=1,Name="pool Villa" },
				new VillaDTO {Id=2,Name="beach Villa" }
			};
	}
}
