using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onti2017v2.Models
{
    public class RezervariModel
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdVacanta { get; set; }
        public DateTime Datasf {  get; set; }
        public DateTime DataSt {  get; set; }
        public int Pret { get; set; }
    }
}
