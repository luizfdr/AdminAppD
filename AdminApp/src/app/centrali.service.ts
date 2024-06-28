import { Injectable } from '@angular/core';
import { Centrali } from './centrali';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CentraliService {

  constructor(private http:HttpClient) { }
  private centraliUrl = "http://192.168.123.24:3000/centrali";

  fetchCentrali ():Observable<Centrali[]>{
    return this.http.get<Centrali[]>(this.centraliUrl);
  }
  fetchCentraleByID(id:string):Observable<Centrali>{
    let url = `${this.centraliUrl}/${id}` ;
    return this.http.get<Centrali>(url);
  }
}
