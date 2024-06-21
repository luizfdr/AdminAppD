using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public class localita
    {
        public string indirizzo { get; set; }
        public double [] coordinate { get; set; }
        public localita() { }
        public localita(string via, double[] coordinate)
        {
            this.indirizzo = via;
            this.coordinate = coordinate;
        }
    }
}
