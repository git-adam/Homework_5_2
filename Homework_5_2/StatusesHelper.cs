using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_5_2
{
    public class StatusesHelper
    {
        public static List<Status> GetStatuses(string defaultStatus)
        {
            return new List<Status>
            {
                new Status() {Id = 0, Name = defaultStatus},
                new Status() {Id = 1, Name = "Na urlopie wyp."},
                new Status() {Id = 2, Name = "Na urlopie zdr."},
                new Status() {Id = 3, Name = "W delegacji"},
                new Status() {Id = 4, Name = "Zwolniony"},
                new Status() {Id = 5, Name = "Dostępny"},
            };
        }
    }
}
