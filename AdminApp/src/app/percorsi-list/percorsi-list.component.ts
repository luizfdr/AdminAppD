import { Component } from '@angular/core';
import { AdminService } from '../admin.service';
import { CommonModule } from '@angular/common';
import { Percorsi } from '../percorsi';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-percorsi-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './percorsi-list.component.html',
  styleUrls: ['./percorsi-list.component.css']
})
export class PercorsiListComponent {
  percorsiList$: Observable<Percorsi[]>;

  constructor(public adminService: AdminService) {
    this.percorsiList$ = this.adminService.percorsiList$;
  }

  selectItem(item: Percorsi) {
    this.adminService.selectItem(item);
  }
}
