import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Solicitud } from '../dtos/solicitud-subasta';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SolicitudService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  get(): Observable<any[]> {
    return this.http.get<any[]>(environment.endpointSolicitud).pipe(
      map((data: Solicitud[]) => data));
  }

  getDto(id): Observable<any> {
    return this.http.get<any>(environment.endpointSolicitud + '/Get/' + id).pipe(
      map((data: any) => data));
  }

  aceptar(dto: Solicitud): Observable<any> {
    return this.http.post<any>(environment.endpointSolicitud + '/Aceptar/', dto, { headers: this.httpHeaders });
  }

  post(dto: Solicitud): Observable<any> {
    return this.http.post<any>(environment.endpointSolicitud, dto, { headers: this.httpHeaders });
  }

  put(dto: Solicitud): Observable<any> {
    return this.http.put<any>(environment.endpointSolicitud, dto, { headers: this.httpHeaders });
  }

  delete(id: string): Observable<any> {
    return this.http.delete<any>(environment.endpointSolicitud + '/' + id, { headers: this.httpHeaders });
  }
}
