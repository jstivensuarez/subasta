import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Puja } from '../dtos/puja';
import { map } from 'rxjs/internal/operators/map';

@Injectable({
  providedIn: 'root'
})
export class PujaService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getGanador(loteId): Observable<any> {
    return this.http.get<any>(environment.endpointPuja + '/GetGanador/'+loteId).pipe(
      map((data: any) => data));
  }

  get(): Observable<any[]> {
    return this.http.get<any[]>(environment.endpointPuja).pipe(
      map((data: Puja[]) => data));
  }

  getDto(id): Observable<any> {
    return this.http.get<any>(environment.endpointPuja + '/Get/' + id).pipe(
      map((data: any) => data));
  }

  confirmarGanador(loteId): Observable<any> {
    return this.http.post<any>(environment.endpointPuja+"/ConfirmarGanador/"+loteId, { headers: this.httpHeaders });
  }

  post(dto: Puja): Observable<any> {
    return this.http.post<any>(environment.endpointPuja, dto, { headers: this.httpHeaders });
  }

  put(dto: Puja): Observable<any> {
    return this.http.put<any>(environment.endpointPuja, dto, { headers: this.httpHeaders });
  }

  delete(id: string): Observable<any> {
    return this.http.delete<any>(environment.endpointPuja + '/' + id, { headers: this.httpHeaders });
  }
}
