using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public class dettagli_centrali
    {
        /// <summary>
        /// Id univoco per identificare la caratteristica
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Nome della caratteristica Es. Potenza
        /// </summary>
        public string tipo {  get; set; }
        /// <summary>
        /// Valore della caratterstica
        /// </summary>
        public string valore { get; set; } 
        /// <summary>
        /// Unità di misura della caratteristica
        /// </summary>
        public string unitaDiMisura { get; set; }

        public dettagli_centrali() { }
        public dettagli_centrali(int id, string tipo, string valore, string unitaDiMisura)
        {
            this.id = id;
            this.tipo = tipo;
            this.valore = valore;
            this.unitaDiMisura = unitaDiMisura;
        }
    }
}
