using Locations.APP.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly LocationsDb _db;

        private readonly IWebHostEnvironment _environment;

        public DatabaseController(LocationsDb db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [HttpGet, Route("~/api/SeedDb")]
        public IActionResult Seed()
        {
            _db.Cities.RemoveRange(_db.Cities.ToList());
            _db.Countries.RemoveRange(_db.Countries.ToList());

            _db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Cities';");
            _db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Countries';");

            _db.Countries.Add(new Country
            {
                Guid = Guid.NewGuid().ToString(),
                CountryName = "Türkiye",
                Cities = new List<City>
                {
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Adana" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Adıyaman" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Afyonkarahisar" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Ağrı" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Amasya" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Ankara" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Antalya" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Artvin" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Aydın" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Balıkesir" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Bilecik" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Bingöl" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Bitlis" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Bolu" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Burdur" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Bursa" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Çanakkale" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Çankırı" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Çorum" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Denizli" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Diyarbakır" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Edirne" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Elazığ" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Erzincan" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Erzurum" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Eskişehir" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Gaziantep" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Giresun" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Gümüşhane" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Hakkari" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Hatay" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Isparta" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Mersin" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "İstanbul" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "İzmir" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Kars" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Kastamonu" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Kayseri" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Kırklareli" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Kırşehir" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Kocaeli" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Konya" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Kütahya" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Malatya" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Manisa" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Kahramanmaraş" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Mardin" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Muğla" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Muş" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Nevşehir" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Niğde" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Ordu" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Rize" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Sakarya" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Samsun" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Siirt" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Sinop" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Sivas" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Tekirdağ" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Tokat" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Trabzon" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Tunceli" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Şanlıurfa" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Uşak" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Van" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Yozgat" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Zonguldak" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Aksaray" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Bayburt" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Karaman" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Kırıkkale" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Batman" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Şırnak" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Bartın" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Ardahan" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Iğdır" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Yalova" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Karabük" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Kilis" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Osmaniye" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Düzce" }
                }
            });

            _db.Countries.Add(new Country
            {
                Guid = Guid.NewGuid().ToString(),
                CountryName = "United States of America",
                Cities = new List<City>
                {
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "New York" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Los Angeles" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Chicago" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Houston" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Phoenix" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Philadelphia" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "San Antonio" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "San Diego" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Dallas" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "San Jose" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Austin" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Jacksonville" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Fort Worth" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Columbus" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Charlotte" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "San Francisco" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Indianapolis" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Seattle" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Denver" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Washington" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Boston" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "El Paso" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Nashville" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Detroit" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Oklahoma City" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Portland" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Las Vegas" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Memphis" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Louisville" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Baltimore" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Milwaukee" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Albuquerque" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Tucson" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Fresno" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Mesa" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Sacramento" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Atlanta" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Kansas City" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Colorado Springs" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Miami" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Raleigh" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Omaha" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Long Beach" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Virginia Beach" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Oakland" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Minneapolis" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Tulsa" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "Arlington" },
                    new City { Guid = Guid.NewGuid().ToString(), CityName = "New Orleans" }
                }
            });

            _db.Countries.Add(new Country
            {
                Guid = Guid.NewGuid().ToString(),
                CountryName = "China",
            });

            _db.SaveChanges();

            return Ok("Database seed successful.");
        }
    }
}
