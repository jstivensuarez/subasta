import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map, catchError, tap } from 'rxjs/operators';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Departamento } from '../dtos/departamento';

@Injectable({
  providedIn: 'root'
})
export class DepartamentoService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
  
  constructor(private http: HttpClient) { }

  getDepartamentos(): Observable<any> {
    return this.http.get(environment.endpointDepartamentos).pipe(
      map((data: Departamento[]) => data));
  }

  getDto(id): Observable<any> {
    return this.http.get<any>(environment.endpointDepartamentos + '/Get/' + id).pipe(
      map((data: any) => data));
  }

  post(dto: Departamento): Observable<any> {
    return this.http.post<any>(environment.endpointDepartamentos, dto, { headers: this.httpHeaders });
  }

  put(dto: Departamento): Observable<any> {
    return this.http.put<any>(environment.endpointDepartamentos, dto, { headers: this.httpHeaders });
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(environment.endpointDepartamentos + '/' + id, { headers: this.httpHeaders });
  }
}
