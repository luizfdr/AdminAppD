import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Centrali } from './centrali';
import { Percorsi } from './percorsi';

@Injectable({
  providedIn: 'root'
})



export class AdminService {
  centraliList: Centrali[] = [];
   private percorsiList: Percorsi[] = [];

  private percorsiSubject = new Observable<Percorsi[]>();

  selectedItemSubject = new Observable<Centrali | Percorsi | null>();
  

  constructor() {}

  addCentrali(centrali: Centrali) {
    // Remove trimming logic if it was causing issues
    this.centraliList.push(centrali);
  }

  addPercorsi(percorsi: Percorsi) {
    // Remove trimming logic if it was causing issues
    this.percorsiList.push(percorsi);
  }

  deleteItem(item: Centrali | Percorsi) {
      this.centraliList = this.centraliList.filter(c => c.id !== item.id);
      this.percorsiList = this.percorsiList.filter(p => p.id !== item.id);
  }

  selectItem(item: Centrali | Percorsi) {
    selectedItem = item;
    console.log("admin service selected item done");
  }
  selectedItemGet(): Centrali | Percorsi | null{
    return selectedItem;
  }

  updateCentrali(updatedCentrali: Centrali) {
    const index = this.centraliList.findIndex(c => c.id === updatedCentrali.id);
    if (index !== -1) {
      this.centraliList[index] = updatedCentrali;
    }
  }

  updatePercorsi(updatedPercorsi: Percorsi) {
    const index = this.percorsiList.findIndex(p => p.id === updatedPercorsi.id);
    if (index !== -1) {
      this.percorsiList[index] = updatedPercorsi;
    }
  }
}
var selectedItem: Centrali | Percorsi | null = null;