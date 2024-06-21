using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    /// <summary>
    /// Oggetto che identifica un immagine con relativo percorso della immagine e un stringa alternativa in caso di errore
    /// </summary>
    public class immagine
    {
        /// <summary>
        /// Percorso della immagine
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// Stringa alternativa stampata in caso di errore
        /// </summary>
        public string alt { get; set; }
        public immagine() { }
        public immagine(string url, string alt)
        {
            this.url = url;
            this.alt = alt;
        }
    }
}
