using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Villa_API.Data;
using Villa_API.Models;
using Villa_API.Models.Dto;

namespace Villa_API.Controllers
{
	[Route("api/VillaAPI")]
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<VillaDTO> GetVillas()
		{
			return VillaStore.villalist;

		}
		[HttpGet("{id:int}")]
		public VillaDTO GetVilla(int id)
		{
			return VillaStore.villalist.FirstOrDefault(u=>u.Id==id);

		}
	}
}
