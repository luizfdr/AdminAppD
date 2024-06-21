import { Component } from '@angular/core';
import { AdminService } from '../admin.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Centrali } from '../centrali';
import { Percorsi } from '../percorsi';

@Component({
  selector: 'app-admin-menu',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-menu.component.html',
  styleUrls: ['./admin-menu.component.css']
})
export class AdminMenuComponent {
  selectedItem: Centrali | Percorsi | null = null;

  constructor(public adminService: AdminService) {
    this.adminService.selectedItem$.subscribe(item => {
      this.selectedItem = item;
    });
  }

  save() {
    if (this.selectedItem) {
      if ((this.selectedItem as Centrali).nome !== undefined) {
        this.adminService.updateCentrali(this.selectedItem as Centrali);
      } else {
        this.adminService.updatePercorsi(this.selectedItem as Percorsi);
      }
    }
  }

  cancel() {
    this.selectedItem = null;
  }

  delete() {
    if (this.selectedItem) {
      this.adminService.deleteItem(this.selectedItem);
      this.selectedItem = null;
    }
  }
}
