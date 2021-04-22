using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PinkRoccade.BS.Classes;
using PinkRoccade.BS.Models;

namespace PinkRoccade.Controllers
{
    public class IncidentsController : Controller
    {

        // GET: Incidents
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateIncident(PinkRoccade.BS.Models.IncidentModel incidentModel)
        {
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

            string mailContent = $"{incidentModel.Description} {img_tag??""}";
            string mailadres_sender = (string)TempData.Peek("email");
            MailHelper.SendMail(mailadres_sender, "Mailbox@Pinkrocadde.nl", incidentModel.Location, mailContent);
            incidentModel.Img_Data = Base64string;
            incidentModel.User_Id = Convert.ToInt32(TempData.Peek("unique_id"));
            SaveIncident.Store_Incident(incidentModel);

            return RedirectToAction("Index");
        }

    }
}