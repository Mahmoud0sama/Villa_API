﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using Villa_Utility;
using Villa_Web.Models;
using Villa_Web.Models.Dto;
using Villa_Web.Models.VM;
using Villa_Web.Services;
using Villa_Web.Services.IServices;

namespace Villa_Web.Controllers
{
	public class VillaNumberController : Controller
	{
		private readonly IVillaNumberService _villaNumberService;
		private readonly IVillaService _villaService;
		private readonly IMapper _mapper;
		public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper, IVillaService villaService)
		{
			_villaNumberService = villaNumberService;
			_villaService = villaService;
			_mapper = mapper;
		}
		public async Task<IActionResult> IndexVillaNumber()
		{
			List<VillaNumberDTO> list = new();
			var response = await _villaNumberService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
			if (response != null && response.IsSuccessfull)
			{
				list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
			}

			return View(list);
		}
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> CreateVillaNumber()
		{
			VillaNumberCreateVM villaNumberVM = new();
			var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
			if (response != null && response.IsSuccessfull)
			{
				villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
					(Convert.ToString(response.Result)).Select(u => new SelectListItem
					{
						Text = u.Name,
						Value = u.Id.ToString()
					});
			}
			return View(villaNumberVM);
		}
		[HttpPost]
		[Authorize(Roles = "admin")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
		{
			if (ModelState.IsValid)
			{
				var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber, HttpContext.Session.GetString(SD.SessionToken));
				if (response != null && response.IsSuccessfull)
				{
					return RedirectToAction(nameof(IndexVillaNumber));
				}
				else
				{
					if (response.ErrorMessages.Count > 0)
					{
						ModelState.AddModelError("ErrorMessages",response.ErrorMessages.FirstOrDefault());
					}
				}
			}
			var res = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
			if (res != null && res.IsSuccessfull)
			{
				model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
					(Convert.ToString(res.Result)).Select(u => new SelectListItem
					{
						Text = u.Name,
						Value = u.Id.ToString()
					});
			}
			return View(model);
		}
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> UpdateVillaNumber(int villaNo)
		{
			VillaNumberUpdateVM villaNumberVM = new();
			var response = await _villaNumberService.GetAsync<APIResponse>(villaNo, HttpContext.Session.GetString(SD.SessionToken));
			if (response != null && response.IsSuccessfull)
			{
				VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
				villaNumberVM.VillaNumber = _mapper.Map<VillaNumberUpdateDTO>(model);
			}
			 response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
			if (response != null && response.IsSuccessfull)
			{
				villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
					(Convert.ToString(response.Result)).Select(u => new SelectListItem
					{
						Text = u.Name,
						Value = u.Id.ToString()
					});
				return View(villaNumberVM);
			}
			return NotFound();
		}
		[HttpPost]
		[Authorize(Roles = "admin")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM model)
		{
			if (ModelState.IsValid)
			{
				var response = await _villaNumberService.UpdateAsync<APIResponse>(model.VillaNumber, HttpContext.Session.GetString(SD.SessionToken));
				if (response != null && response.IsSuccessfull)
				{
					return RedirectToAction(nameof(IndexVillaNumber));
				}
				else
				{
					if (response.ErrorMessages.Count > 0)
					{
						ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
					}
				}
			}
			var res = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
			if (res != null && res.IsSuccessfull)
			{
				model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
					(Convert.ToString(res.Result)).Select(u => new SelectListItem
					{
						Text = u.Name,
						Value = u.Id.ToString()
					});
			}
			return View(model);
		}
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> DeleteVillaNumber(int villaNo)
		{
			VillaNumberDeleteVM villaNumberVM = new();
			var response = await _villaNumberService.GetAsync<APIResponse>(villaNo, HttpContext.Session.GetString(SD.SessionToken));
			if (response != null && response.IsSuccessfull)
			{
				VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
				villaNumberVM.VillaNumber = model;
			}
			response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
			if (response != null && response.IsSuccessfull)
			{
				villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
					(Convert.ToString(response.Result)).Select(u => new SelectListItem
					{
						Text = u.Name,
						Value = u.Id.ToString()
					});
				return View(villaNumberVM);
			}
			return NotFound();
		}
		[HttpPost]
		[Authorize(Roles = "admin")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVM model)
		{
			var response = await _villaNumberService.DeleteAsync<APIResponse>(model.VillaNumber.VillaNo, HttpContext.Session.GetString(SD.SessionToken));
			if (response != null && response.IsSuccessfull)
			{
				return RedirectToAction(nameof(IndexVillaNumber));
			}
			return View(model);
		}
	}
}
