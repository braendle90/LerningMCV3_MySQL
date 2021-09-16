using LerningMCV3_MySQL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LerningMCV3_MySQL.ViewModel;
using LerningMCV3_MySQL.Models;
using System.Net;
using LerningMCV3_MySQL.Services;
using Microsoft.AspNetCore.Authorization;
using MimeKit;

namespace LerningMCV3_MySQL.Controllers
{
    
    public class PersonViewModelController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMailService _mailService;

        public PersonViewModelController(ApplicationDbContext context, IMailService mailService)
        {
            this.context = context;
            this._mailService = mailService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {


            var mailModel = new MailRequest();
            var message = new MimeMessage();
     

            var builder = new BodyBuilder();

            mailModel.ToMail = "d.braendle@aub.at";
            mailModel.Subject = "Test Mail ASP";

           // mailModel.Attachments = 

           // mailModel.Attachments = builder.Attachments.Add(@"C:\Users\DCV\Desktop\CodingCampus\MCV Lerning\LerningMCV3_MySQL\LerningMCV3_MySQL\Controllers\cars.dump.txt");




            // mailModel.Attachments.Add (@"C:\Users\DCV\Desktop\CodingCampus\MCV Lerning\LerningMCV3_MySQL\LerningMCV3_MySQL\Controllers\cars.dump.txt");

            _mailService.SendEmailAsync(mailModel);


            var pax = await context.Persons
                .Include(x => x.Adress)
                .Include(x => x.Pet)
                .ToListAsync();

            return View(pax);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Street,City,Type,Name")] PersonViewModel person)
        {

            if (ModelState.IsValid)
            {

                Person pax = new Person
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName
                };

                Adress adress = new Adress
                {

                    City = person.City,
                    Street = person.Street
                };

                Pet pet = new Pet
                {

                    Type = person.Type,
                    Name = person.Name
                };

               
                context.Add(adress);
                context.Add(pet);
                context.SaveChanges();

                pax.AdressId = adress.Id;
                pax.PetId = pet.Id;

                context.Add(pax);
                context.SaveChanges();

              

                return RedirectToAction("Index");


            }
           

            return View(person);

        }

        public IActionResult Edit (int? id)
        {

            if(id == null)
            {
                return NotFound();
            }

            Person person = context.Persons.Find(id);
            Adress adress = context.Adresses.Find(person.AdressId);
            Pet pet = context.Pets.Find(person.PetId);

            var pvm = new PersonViewModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Street = adress.Street,
                City = adress.City,
                Type = pet.Type,
                Name = pet.Name


            };

            if(person == null)
            {
                return NotFound();
            }

            return View(pvm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit ([Bind("Id,FirstName,LastName,Street,City,Type,Name")] PersonViewModel personViewModel)
        {
            if (ModelState.IsValid)
            {


                Person pax = new Person
                {
                    Id = personViewModel.Id,
                    FirstName = personViewModel.FirstName,
                    LastName = personViewModel.LastName

                };

                Adress adress = new Adress
                {
                    Id = pax.AdressId,
                    Street = personViewModel.Street,
                    City = personViewModel.City
                };

                Pet pet = new Pet
                {
                    Id = pax.PetId,
                    Name = personViewModel.Name,
                    Type = personViewModel.Type
                };

                context.Update(adress);
                context.Update(pet);
                context.SaveChanges();


                pax.AdressId = adress.Id;
                pax.PetId = pet.Id;
                context.Update(pax);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(personViewModel);




        }

        public IActionResult Delete(int? Id)
        {

            PersonViewModel pvm = new PersonViewModel();

            Person pax = context.Persons.Find(Id);
            Adress adress = context.Adresses.Find(pax.AdressId);
            Pet pet = context.Pets.Find(pax.PetId);

            pvm.FirstName = pax.FirstName;
            pvm.LastName = pax.LastName;
            pvm.Street = adress.Street;
            pvm.City = adress.City;
            pvm.Type = pet.Type;
            pvm.Name = pet.Name;



            return View(pvm);

            }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed (int Id)
        {

            Person pax = context.Persons.Find(Id);
            Adress adress = context.Adresses.Find(pax.AdressId);
            Pet pet = context.Pets.Find(pax.PetId);

            context.Persons.Remove(pax);
            context.SaveChanges();
            context.Pets.Remove(pet);
            context.SaveChanges();
            context.Adresses.Remove(adress);
            context.SaveChanges();


            return RedirectToAction("Index");

        }



        public IActionResult Details(int? id)
        {




            if (id == null)
            {
                return NotFound();
            }
            var person = context.Persons.Find(id);
            var adress = context.Adresses.Find(person.AdressId);
            var pet = context.Pets.Find(person.PetId);
            var personViewModel = new PersonViewModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Street = adress.Street,
                City = adress.City,
                Type = pet.Type,
                Name = pet.Name
            };

            if (person == null)
            {
                return NotFound();
            }
            return View(personViewModel);
        }


    }
}
