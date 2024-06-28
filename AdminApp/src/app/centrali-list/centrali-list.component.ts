import { Component } from '@angular/core';
import { AdminService } from '../admin.service';
import { CommonModule } from '@angular/common';
import { Centrali } from '../centrali';
import { Observable } from 'rxjs';
import { CentraliService } from '../centrali.service';

@Component({
  selector: 'app-centrali-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './centrali-list.component.html',
  styleUrls: ['./centrali-list.component.css']
})
export class CentraliListComponent {
  centraliList: Centrali[] = [];
  centraliListDefinitive: Centrali[] = [];

  constructor(public adminService: AdminService, public centraliService: CentraliService) {
  }

  selectItem(item: Centrali) {
    this.adminService.selectItem(item);
  }

  ngOnInit():void{
    this.getCentrali();
  }

  getCentrali():void{
    this.centraliService.fetchCentrali().subscribe(tmpCentraliList => {
      this.centraliList = tmpCentraliList;

    });
  }
}
