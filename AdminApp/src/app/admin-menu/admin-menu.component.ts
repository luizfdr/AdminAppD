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
    this.selectedItem = this.adminService.selectedItemGet();
    console.log("menu selecte");
  }

  save() {
    if (this.selectedItem) {
      if ((this.selectedItem as Centrali).nome !== undefined) {
        this.adminService.addCentrali(this.selectedItem as Centrali);
        this.adminService.updateCentrali(this.selectedItem as Centrali);
      } else if((this.selectedItem as Percorsi).nome !== undefined) {
        this.adminService.addPercorsi(this.selectedItem as Percorsi);
        this.adminService.updatePercorsi(this.selectedItem as Percorsi);
      }
    }
    this.selectedItem = null;
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
