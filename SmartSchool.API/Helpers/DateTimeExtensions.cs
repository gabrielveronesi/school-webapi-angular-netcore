using System;

namespace SmartSchool.API.Helpers
{
    public static class DateTimeExtensions //toda classe com metodo statico precisa ser statico a class
    {
        public static int GetCurrentAge (this DateTime dateTime) //pegar a idade atual
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTime.Year;
            // A data atual que eestá sendo trabalhada menos(subtraido) pelo ano 
            // 2020 - 1996 = a idade!!
            if (currentDate < dateTime.AddYears(age))
                age--;

                return age;
        }
    }
}