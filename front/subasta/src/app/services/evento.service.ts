import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Subasta } from '../dtos/subasta';
import { map } from 'rxjs/operators';
import { Evento } from '../dtos/evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
  
  constructor(private http: HttpClient) { }


  getForClients(): Observable<any[]> {
    return this.http.get<any[]>(environment.endpointEvento + '/GetForClients').pipe(
      map((data: Evento[]) => data));
  }

  get(): Observable<any[]> {
    return this.http.get<any[]>(environment.endpointEvento).pipe(
      map((data: Evento[]) => data));
  }

  getDto(id): Observable<any> {
    return this.http.get<any>(environment.endpointEvento + '/Get/' + id).pipe(
      map((data: any) => data));
  }

  post(dto: Evento): Observable<any> {
    return this.http.post<any>(environment.endpointEvento, dto, { headers: this.httpHeaders });
  }

  put(dto: Evento): Observable<any> {
    return this.http.put<any>(environment.endpointEvento, dto, { headers: this.httpHeaders });
  }

  delete(id: string): Observable<any> {
    return this.http.delete<any>(environment.endpointEvento + '/' + id, { headers: this.httpHeaders });
  }
}
