using System;
using System.Collections.Generic;
using System.Data;

namespace VinylRecordsApplication_Тепляков.Classes
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static IEnumerable<Country> AllCountries()
        {
            List<Country> countries = new List<Country>();
            if (Pages.ChangeSettings.ConnectIsApply == true)
            {
                DataTable requestCountrys = DBConnection.Connection("Select * From [dbo].[Country]");
                foreach (DataRow row in requestCountrys.Rows)
                    countries.Add(new Country()
                    {
                        Id = Convert.ToInt32(row[0]),
                        Name = Convert.ToString(row[1])
                    });
                return countries;
            }
            return null;
        }
    }
}
