import { Component } from '@angular/core';
import { AdminComponent } from './admin/admin.component';
import { PercorsiListComponent } from './percorsi-list/percorsi-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [AdminComponent, PercorsiListComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AdminApp';
}
