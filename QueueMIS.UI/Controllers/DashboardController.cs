﻿using Microsoft.AspNetCore.Mvc;
using QueueMIS.Services;
using QueueMIS.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace QueueMIS.Controllers
{
    public class DashboardController : Controller
    {
       
        private readonly IBookingManager _bookingManager;

        public DashboardController(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
            
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.PatientCount = _bookingManager.CountPatients();
            ViewBag.PatientWithCardioCount = _bookingManager.PatientsWithServiceType("Cardio-Vascular");
            ViewBag.PatientWithDentalCount = _bookingManager.PatientsWithServiceType("Dental");
            ViewBag.PatientWithOpticalCount = _bookingManager.PatientsWithServiceType("Optical");
            ViewBag.NumberOfServedPatient = _bookingManager.NumberOfServedPatients("Served");
            ViewBag.NumberOfNotServedPatient = _bookingManager.NumberOfServedPatients("Not Yet Served");

            var bookings = await _bookingManager.GetPatientBookingsInQueueAsync();
            return View(bookings);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
