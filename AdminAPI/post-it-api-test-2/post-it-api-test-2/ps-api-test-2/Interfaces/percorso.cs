using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public class percorso
    {
        /// <summary>
        /// Id univoco che indetifica un percorso
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Stringa che identifica la valle di appartenza Es. AGN
        /// </summary>
        public string zona { get; set; }  
        /// <summary>
        /// Nome del percorso
        /// </summary>
        public string nome { get; set; }
        /// <summary>
        /// Descrizione del percorso
        /// </summary>
        public string descrizione { get; set; }
        /// <summary>
        /// Dettagli di un percorso 
        /// </summary>
        public int[] centrali { get; set; }
        public immagine[] immagini { get; set; }
        public dettagli_percorso dettagliPercorsi { get; set; }
        
        public percorso() { }
        public percorso(int id, string zona, string nome, string descrizione, dettagli_percorso percorsi, int[] centrali, immagine[] immagini)
        {
            this.id = id;
            this.zona = zona;
            this.nome = nome;
            this.descrizione = descrizione;
            this.dettagliPercorsi = percorsi;
            this.centrali = centrali;
            this.immagini = immagini;
        }
    }
}
