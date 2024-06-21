using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public class centrale
    {
        /// <summary>
        /// Id univoco che indetifica la centrale
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// True = Centrale attiva False = Centrale inattiva
        /// </summary>
        public bool stato { get; set; }
        /// <summary>
        /// Stringa che identifica la valle di appartenza Es. AGN
        /// </summary>
        public string zona { get; set; }
        /// <summary>
        /// Nome della centrale
        /// </summary>
        public string nome { get; set; }
        /// <summary>
        /// Descrizione della centrale
        /// </summary>
        public string descrizione { get; set; }
        /// <summary>
        /// indirizzo della centrale
        /// </summary>
        public localita localita{ get; set; }
        /// <summary>
        /// Array di immagini allegate alla centrale
        /// </summary>
        public immagine[] immagini { get; set; }
        /// <summary>
        /// Array che contiene i dettagli delle centrali
        /// </summary>
        public dettagli_centrali[] dettagliCentrali { get; set; }
        /// <summary>
        /// Array che contiene gli Id dei percorsi che passano per la centrale
        /// </summary>
        public int[] percorsi { get; set; }

        public centrale() { }
        public centrale(int id , string zona ,bool stato, string nome , string descrizione , localita indirizzo , immagine[] immagini , dettagli_centrali[] dettagliCentrali , int[] percorsi )
        {
            this.id = id;
            this.zona = zona;
            this.stato = stato;
            this.nome = nome;
            this.descrizione = descrizione;
            this.immagini = immagini;
            this.dettagliCentrali = dettagliCentrali;
            this.percorsi = percorsi;
            this.localita = indirizzo;
        }
    }
}
