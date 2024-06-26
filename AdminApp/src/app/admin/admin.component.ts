import { Component } from '@angular/core';
import { AdminService } from '../admin.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CentraliListComponent } from '../centrali-list/centrali-list.component';
import { PercorsiListComponent } from '../percorsi-list/percorsi-list.component';
import { AdminMenuComponent } from '../admin-menu/admin-menu.component';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, FormsModule, CentraliListComponent, PercorsiListComponent, AdminMenuComponent],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {
  currentListType: 'centrali' | 'percorsi' = 'centrali';

  constructor(public adminService: AdminService) {}

  switchList(type: 'centrali' | 'percorsi') {
    this.currentListType = type;
  }

  addItem() {
    if (this.currentListType === 'centrali') {
      const newCentrali = {
        id: Date.now(),
        zona: '',
        nome: '',
        stato: true,
        descrizione: '',
        localita: {} as any,
        immagini: [],
        dettagliCentrali: [],
        percorsi: [],
      };
      this.adminService.addCentrali(newCentrali);
      this.adminService.selectItem(newCentrali);
    } else if (this.currentListType === 'percorsi') {
      const newPercorsi = {
        id: Date.now(),
        zona: '',
        nome: '',
        descrizione: '',
        dettagliPercorsi: [] as any,
        centrali: [],
        immagini: [] as any,
      };
      this.adminService.addPercorsi(newPercorsi);
      this.adminService.selectItem(newPercorsi);
    }
  }
}
