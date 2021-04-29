﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PinkRoccade.BS.Classes;
using PinkRoccade.BS.Models;

namespace PinkRoccade.Controllers
{
    public class IncidentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly IDNTCaptchaValidatorService _validatorService;
        public IncidentsController(IDNTCaptchaValidatorService validatorService)
        {
            _validatorService = validatorService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(
        ErrorMessage = "Vul de juiste code in.",
        CaptchaGeneratorLanguage = Language.English,
        CaptchaGeneratorDisplayMode = DisplayMode.ShowDigits)]

        [HttpPost]
        public IActionResult CreateIncident(IncidentModel incidentModel)
        {
			if (ModelState.IsValid)
			{
                if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                {
                    this.ModelState.AddModelError("", "Vul de juiste code in.");
                    return View("Index");
                }

                UserModel user = SessionHelper.GetObjectFromJson<UserModel>(HttpContext.Session, "_User");

                string img_tag = null;
                string Base64string = null;
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files[0];
                    if (file.ContentType.Contains("img"))
                    {
                        // idk ites
                    }

                    BinaryReader br = new BinaryReader(file.OpenReadStream());
                    Byte[] byteImage = br.ReadBytes((Int32)file.Length);
                    Base64string = Convert.ToBase64String(byteImage, 0, byteImage.Length);

                    img_tag = $"<img src='data:image/png;base64,{Base64string}'/>";
                }

                string mailContent = $"{incidentModel.Description} {img_tag ?? ""}";
                string mailadres_sender = user.EMail;
                MailHelper.SendMail((string)mailadres_sender, "Mailbox@Pinkrocadde.nl", incidentModel.Location, mailContent);
                incidentModel.Img_Data = Base64string;
                incidentModel.User_Id = user.Unique_id;
                SaveIncident.Store_Incident(incidentModel);

                return RedirectToAction("Index");
			}
			else
			{
                return View("Index", incidentModel);
			}
         
        }


    }
}