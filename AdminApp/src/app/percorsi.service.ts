import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Percorsi } from './percorsi';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PercorsiService {

  constructor(private http:HttpClient) { }
  private percorsiUrl = "http://192.168.123.24:3000/percorsi";

  fetchPercorsi():Observable<Percorsi[]>{
    return this.http.get<Percorsi[]>(this.percorsiUrl);
  };
  fetchPercorsiByID(id:string):Observable<Percorsi>{
    let url = `${this.percorsiUrl}/${id}` ;
    return this.http.get<Percorsi>(url);
  }
}
