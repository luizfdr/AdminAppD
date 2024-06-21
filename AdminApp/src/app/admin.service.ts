import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Centrali } from './centrali';
import { Percorsi } from './percorsi';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private centraliList: Centrali[] = [];
  private percorsiList: Percorsi[] = [];

  private centraliSubject = new BehaviorSubject<Centrali[]>(this.centraliList);
  private percorsiSubject = new BehaviorSubject<Percorsi[]>(this.percorsiList);

  private selectedItemSubject = new BehaviorSubject<Centrali | Percorsi | null>(null);

  centraliList$ = this.centraliSubject.asObservable();
  percorsiList$ = this.percorsiSubject.asObservable();
  selectedItem$ = this.selectedItemSubject.asObservable();

  constructor() {}

  addCentrali(centrali: Centrali) {
    // Remove trimming logic if it was causing issues
    this.centraliList.push(centrali);
    this.centraliSubject.next(this.centraliList);
  }

  addPercorsi(percorsi: Percorsi) {
    // Remove trimming logic if it was causing issues
    this.percorsiList.push(percorsi);
    this.percorsiSubject.next(this.percorsiList);
  }

  deleteItem(item: Centrali | Percorsi) {
      this.centraliList = this.centraliList.filter(c => c.id !== item.id);
      this.centraliSubject.next(this.centraliList);
      this.percorsiList = this.percorsiList.filter(p => p.id !== item.id);
      this.percorsiSubject.next(this.percorsiList);
  }

  selectItem(item: Centrali | Percorsi) {
    this.selectedItemSubject.next(item);
  }

  updateCentrali(updatedCentrali: Centrali) {
    const index = this.centraliList.findIndex(c => c.id === updatedCentrali.id);
    if (index !== -1) {
      this.centraliList[index] = updatedCentrali;
      this.centraliSubject.next(this.centraliList);
    }
  }

  updatePercorsi(updatedPercorsi: Percorsi) {
    const index = this.percorsiList.findIndex(p => p.id === updatedPercorsi.id);
    if (index !== -1) {
      this.percorsiList[index] = updatedPercorsi;
      this.percorsiSubject.next(this.percorsiList);
    }
  }
}
