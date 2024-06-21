using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public class dettagli_percorso
    {
        /// <summary>
        /// Id di outdooractive
        /// </summary>
        public string oaUrl { get; set; }

        /// <summary>
        /// Lunghezza in KM del percorso
        /// </summary>        
        public double lunghezza { get; set; }

        /// <summary>
        /// Durata del percorso in minuti 
        /// </summary>
        public double durata { get; set; }

        /// <summary>
        /// Dislivello medio in metri
        /// </summary>
        public double dislivello { get; set; }

        /// <summary>
        /// Punto più alto in metri
        /// </summary>
        public double puntoAlto { get; set; }

        /// <summary>
        /// Punto più basso in metri
        /// </summary>
        public double puntoBasso { get; set; }

        public dettagli_percorso() { }

        public dettagli_percorso(string oaUrl, double lunghezza, double durata, double dislivello, double puntoPiuAlto, double puntoPiuBasso)
        {
            this.oaUrl = oaUrl;
            this.lunghezza = lunghezza;
            this.durata = durata;
            this.dislivello = dislivello;
            this.puntoAlto = puntoPiuAlto;
            this.puntoBasso = puntoPiuBasso;
        }
    }
}
