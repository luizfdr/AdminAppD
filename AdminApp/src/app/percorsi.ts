import { Immagine } from "./immagine";
import { DettagliPercorso } from "./dettagli-percorso";

export interface Percorsi {
  id: number;
  zona: string;
  nome: string;
  descrizione: string;
  dettagliPercorsi: DettagliPercorso;
  centrali: number[];
  immagini: Immagine[];

}
  