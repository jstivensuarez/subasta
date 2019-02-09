import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subasta } from '../dtos/subasta';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SubastaService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
  
  constructor(private http: HttpClient) { }

  
  get(): Observable<any[]> {
    return this.http.get<any[]>(environment.endpointSubasta+ '/GetAll').pipe(
      map((data: Subasta[]) => data));
  }

  getDto(id): Observable<any> {
    return this.http.get<any>(environment.endpointSubasta + '/GetSubasta/' + id).pipe(
      map((data: any) => data));
  }

  post(dto: Subasta): Observable<any> {
    return this.http.post<any>(environment.endpointSubasta, dto, { headers: this.httpHeaders });
  }

  put(dto: Subasta): Observable<any> {
    return this.http.put<any>(environment.endpointSubasta, dto, { headers: this.httpHeaders });
  }

  delete(id: string): Observable<any> {
    return this.http.delete<any>(environment.endpointSubasta + '/' + id, { headers: this.httpHeaders });
  }

  getSubastas(id): Observable<any> {
    return this.http.get(environment.endpointSubasta+'/GetPorEvento/'+ id).pipe(
      map((data: Subasta[]) => data));
  }
}
