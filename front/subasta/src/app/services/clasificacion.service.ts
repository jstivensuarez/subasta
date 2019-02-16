import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Clasificacion } from '../dtos/clasificacion';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ClasificacionService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
  
  constructor(private http: HttpClient) { }

  getClasificaciones(id): Observable<any> {
    return this.http.get(environment.endpointClasificacion+'/GetPorCategoria/'+ id).pipe(
      map((data: Clasificacion[]) => data));
  }

  getDto(id): Observable<any> {
    return this.http.get<any>(environment.endpointClasificacion + '/Get/' + id).pipe(
      map((data: any) => data));
  }

  post(dto: Clasificacion): Observable<any> {
    return this.http.post<any>(environment.endpointClasificacion, dto, { headers: this.httpHeaders });
  }

  put(dto: Clasificacion): Observable<any> {
    return this.http.put<any>(environment.endpointClasificacion, dto, { headers: this.httpHeaders });
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(environment.endpointClasificacion + '/' + id, { headers: this.httpHeaders });
  }
}
