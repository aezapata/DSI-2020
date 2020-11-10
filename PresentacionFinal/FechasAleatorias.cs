using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionFinal
{
    public class FechasAleatorias
    {
        Random randNum = new Random();

        
        public static DateTime GetAleatorio()
        {
            DateTime startDate = new DateTime(2020, 9, 30, 19, 0, 0);
            DateTime endDate = new DateTime(2020, 11, 8, 23, 55, 0);

            TimeSpan timeSpan = endDate - startDate;
            var randomTest = new Random();
            TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
            DateTime newDate = startDate + newSpan;
            return newDate;
        }

        public static DateTime GetAleatorio(DateTime nuevoFin)
        {
            DateTime startDate = new DateTime(2020, 9, 30, 19, 0, 0);
            DateTime endDate = nuevoFin;

            TimeSpan timeSpan = endDate - startDate;
            var randomTest = new Random();
            TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
            DateTime newDate = startDate + newSpan;
            return newDate;
        }
    }
}
