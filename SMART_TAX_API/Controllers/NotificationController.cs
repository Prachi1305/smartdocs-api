﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SMART_TAX_API.Helpers;
using SMART_TAX_API.IServices;
using SMART_TAX_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_TAX_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("GetNotificationList")]
        public ActionResult<Response<List<NOTIFICATION>>> GetNotificationList()
        {
            return Ok(JsonConvert.SerializeObject(_notificationService.notificationList()));
        }

        [HttpGet("GetNotificationCount")]
        public ActionResult<Response<int>> GetTotalDetentionCost()
        {
            return Ok(JsonConvert.SerializeObject(_notificationService.GetNotificationCount()));
        }

        [HttpGet("GetNotificationDetails")]
        public ActionResult<Response<NOTIFICATION>> GetNotificationDetails(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_notificationService.GetNotificationDetails(ID)));
        }

        [HttpGet("ChangeNotificationStatus")]
        public ActionResult<Response<string>> ChangeNotificationStatus(int ID)
        {
            return Ok(JsonConvert.SerializeObject(_notificationService.ChangeNotificationStatus(ID)));
        }
    }
}
