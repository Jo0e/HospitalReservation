using Hospital.Data;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    public class DoctorsController : Controller
    {
        public IActionResult BookAppointment()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var doctors = context.Doctors.ToList();
            return View(doctors);
        }
        public IActionResult CompleteAppointment(int id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var doctor = context.Doctors.Where(e=> e.Id == id).FirstOrDefault();
            if (doctor != null)
            {
                return View(doctor);
            }
            return RedirectToAction("NotFound");
        }

        public IActionResult SuccessReservation()
        {
            if (TempData.ContainsKey("AppointmentId"))
            {
                ApplicationDbContext context = new ApplicationDbContext();
                int appointmentId = (int)TempData["AppointmentId"];
                ViewBag.AppointmentId = appointmentId;
                var appointment = context.Appointments.Where(e => e.Id == appointmentId).FirstOrDefault();
                return View(appointment);
            }
            return RedirectToAction("NotFound");
        }
        public IActionResult NotFound() 
        {
            return View();
        }
        public IActionResult FailedReservation() 
        {
            return View();
        }

        public IActionResult AllAppointment() 
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var appointment = context.Appointments.Include(e=>e.Doctor).ToList();
            return View(appointment);
        }

        public IActionResult RemoveAppointment(int id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var removed = context.Appointments.Where(e=>e.Id == id).FirstOrDefault();
            if (removed != null) {
                context.Appointments.Remove(removed);
                context.SaveChanges();
                return View(removed);
            }
            return RedirectToAction("NotFound");
        }

        [HttpPost]
        public IActionResult CompleteAppointment(string PatientName, DateOnly AppointmentDate, TimeOnly AppointmentTime, int DoctorId)
        {
            if (ModelState.IsValid) 
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var appointment = new Appointment
                {
                    PatientName = PatientName,
                    AppointmentDate = AppointmentDate,
                    AppointmentTime = AppointmentTime,
                    DoctorId = DoctorId
                };
                context.Appointments.Add(appointment);
                context.SaveChanges();

                TempData["AppointmentId"] = appointment.Id;

                return RedirectToAction("SuccessReservation");

            }
            return RedirectToAction("FailedReservation");
        }
    }
}
