import { Component, OnInit } from '@angular/core';
import { AdminService } from '../admin.service';
import { CommonModule } from '@angular/common';
import { Percorsi } from '../percorsi';
import { Observable } from 'rxjs';
import { PercorsiService } from '../percorsi.service';

@Component({
  selector: 'app-percorsi-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './percorsi-list.component.html',
  styleUrls: ['./percorsi-list.component.css']
})
export class PercorsiListComponent implements OnInit{
  percorsiFiltered : Percorsi[]=[]
  percorsiListDefinitive : Percorsi[] = []

  constructor(public adminService: AdminService, public percorsiService: PercorsiService) {
  }

  ngOnInit():void{
    this.getPercorsi();
  };
  selectItem(item: Percorsi) {
    this.adminService.selectItem(item);
  }
  getPercorsi(): void{
    this.percorsiService.fetchPercorsi().subscribe(tmpPercorsiList=> {
      this.percorsiListDefinitive = this.percorsiFiltered = tmpPercorsiList;

    });
  };
}
