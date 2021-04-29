﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using PinkRoccade.BS.Classes;
using PinkRoccade.BS.Models;

namespace PinkRoccade.Controllers
{
    public class IncidentsController : Controller
    {
        public MySqlConnection GetSqlConnection()
        {
            MySqlConnection connectionString = new MySqlConnection("server = localhost; port = 3306; database = pinkroccade; user = root");
            return connectionString;
        }

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
            if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
            {
                this.ModelState.AddModelError("", "Vul de juiste code in.");
                return View("Index");
            }
            UserModel user = SessionHelper.GetObjectFromJson<UserModel>(HttpContext.Session, "_User");
            string img_tag = null;
            string Base64string = null;
            string mailadres_sender = incidentModel.Email;
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
            incidentModel.Img_Data = Base64string;
            if (user == null)
            {
                MySqlConnection conn = GetSqlConnection();
                MySqlCommand getUserData = new MySqlCommand("SELECT `id` FROM user WHERE email=@val1", conn);
                getUserData.Parameters.AddWithValue("@val1", incidentModel.Email);
                try
                {
                    conn.Open();
                    getUserData.Prepare();
                    var executeString = getUserData.ExecuteReader();
                    while (executeString.Read())
                    {
                        var id = executeString.GetInt32(0);
                        incidentModel.User_Id = id;
                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("error: " + e.Message);
                }
            }
            else
            {
                incidentModel.User_Id = user.Unique_id;
            }
            MailHelper.SendMail((string)mailadres_sender, "Mailbox@Pinkrocadde.nl", incidentModel.Location, mailContent);
            SaveIncident.Store_Incident(incidentModel);
            return RedirectToAction("Index");
        }
    }
}