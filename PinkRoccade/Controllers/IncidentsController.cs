using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using PinkRoccade.BS.Classes;
using PinkRoccade.BS.Models;

namespace PinkRoccade.Controllers
{
    public class IncidentsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IDNTCaptchaValidatorService _validatorService;
        public IncidentsController(IConfiguration configuration, IDNTCaptchaValidatorService validatorService) {
            _validatorService = validatorService;
            _configuration = configuration;
        }

        public MySqlConnection GetSqlConnection()
        {
            MySqlConnection connectionString = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return connectionString;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Markers()
        {
            var data1 = GetMapMarkers();
            return Ok(data1);
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
            if (SessionHelper.GetObjectFromJson<UserModel>(HttpContext.Session, "_User")!= null || incidentModel.Email != null)
            {//check if there is an email to use for the incident
                if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                {//check if captcha is left empty
                    this.ModelState.AddModelError("", "Vul de juiste code in.");
                    return View("Index");
                }             
                string img_tag = null;
                string Base64string = null;           
                if (Request.Form.Files.Count > 0)
                {//check if there is a file added
                    IFormFile file = Request.Form.Files[0];
                    BinaryReader br = new BinaryReader(file.OpenReadStream());
                    Byte[] byteImage = br.ReadBytes((Int32)file.Length);
                    Base64string = Convert.ToBase64String(byteImage, 0, byteImage.Length);
                    img_tag = $"<img src='data:image/png;base64,{Base64string}'/>";
                }
                string mailContent = $"{incidentModel.Description} {img_tag ?? ""}";
                incidentModel.Img_Data = Base64string;
                if (SessionHelper.GetObjectFromJson<UserModel>(HttpContext.Session, "_User") == null)
                {//if user isnt logged in get the user id using the email
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
                            if (id != 0)
                            {
                                incidentModel.User_Id = id;
                            }
                        }
                        conn.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("error: " + e.Message);
                    }
                }
                else
                {//if logged in use the email of the logged in user
                    UserModel user = SessionHelper.GetObjectFromJson<UserModel>(HttpContext.Session, "_User");
                    incidentModel.User_Id = user.Unique_id;
                    incidentModel.Email = user.Email;
                }
                string mailadres_sender = incidentModel.Email;
                MailHelper.SendMail((string)mailadres_sender, "Mailbox@Pinkrocadde.nl", incidentModel.Location, mailContent);
                SaveIncident.Store_Incident(incidentModel);
                TempData["Success"] = "Melding succesvol aangemaakt.";
                return RedirectToAction("Index");
            }
            else
            {// give error
                TempData["Danger"] = "Melding is niet aangemaakt.";
                return RedirectToAction("Index", incidentModel);
            }


        }

        public IEnumerable<dynamic> GetMapMarkers()
        {
            MySqlConnection conn = GetSqlConnection();
            MySqlCommand getMarkers = new MySqlCommand("SELECT * FROM alert", conn);
            List<dynamic> markers = new List<dynamic>();

            conn.Open();

            var reader = getMarkers.ExecuteReader();
            while (reader.Read())
            {
                markers.Add(new
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Status_Id = Convert.ToInt32(reader["status_id"]),
                    Location = reader["location"].ToString(),
                    Latitude = reader["latitude"],
                    Longitude = reader["longitude"],
                    Description = reader["description"].ToString(),
                    Img_Data = reader["img_data"].ToString(),
                }); ;
            }

            conn.Close();

            return markers;
        }
    }
}