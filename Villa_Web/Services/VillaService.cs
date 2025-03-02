﻿using Newtonsoft.Json.Linq;
using Villa_Utility;
using Villa_Web.Models;
using Villa_Web.Models.Dto;
using Villa_Web.Services.IServices;

namespace Villa_Web.Services
{
	public class VillaService : BaseService, IVillaService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string villaUrl;
        public VillaService(IHttpClientFactory clientFactory,IConfiguration configuration) : base(clientFactory) 
        {
			_clientFactory = clientFactory;
			villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }
        public Task<T> CreateAsync<T>(VillaCreateDTO dto, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = dto,
				Url = villaUrl+ "/api/VillaAPI",
				Token = token
			});

		}

		public Task<T> DeleteAsync<T>(int id, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = villaUrl + "/api/VillaAPI/"+id,
				Token = token
			});
		}

		public Task<T> GetAllAsync<T>(string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = villaUrl + "/api/VillaAPI",
				Token = token
			});
		}

		public Task<T> GetAsync<T>(int id,string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = villaUrl + "/api/VillaAPI/"+id,
				Token = token
			});
		}

		public Task<T> UpdateAsync<T>(VillaUpdateDTO dto,string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dto,				 
				Url = villaUrl + "/api/VillaAPI/"+dto.Id,
				Token = token
			});
		}
	}
}
