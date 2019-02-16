import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { Raza } from '../dtos/raza';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RazaService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getRazas(id): Observable<any> {
    return this.http.get(environment.endpointRaza+'/GetPorCategoria/'+ id).pipe(
      map((data: Raza[]) => data));
  }

  get(): Observable<any[]> {
    return this.http.get<any[]>(environment.endpointRaza).pipe(
      map((data: Raza[]) => data));
  }

  getDto(id): Observable<any> {
    return this.http.get<any>(environment.endpointRaza + '/Get/' + id).pipe(
      map((data: any) => data));
  }

  post(dto: Raza): Observable<any> {
    return this.http.post<any>(environment.endpointRaza, dto, { headers: this.httpHeaders });
  }

  put(dto: Raza): Observable<any> {
    return this.http.put<any>(environment.endpointRaza, dto, { headers: this.httpHeaders });
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(environment.endpointRaza + '/' + id, { headers: this.httpHeaders });
  }
}
