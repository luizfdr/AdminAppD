import { Component } from '@angular/core';
import { AdminService } from '../admin.service';
import { CommonModule } from '@angular/common';
import { Centrali } from '../centrali';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-centrali-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './centrali-list.component.html',
  styleUrls: ['./centrali-list.component.css']
})
export class CentraliListComponent {
  centraliList$: Observable<Centrali[]>;

  constructor(public adminService: AdminService) {
    this.centraliList$ = this.adminService.centraliList$;
  }

  selectItem(item: Centrali) {
    this.adminService.selectItem(item);
  }
}
