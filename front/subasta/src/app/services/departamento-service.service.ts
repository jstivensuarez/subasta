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

  constructor(private http: HttpClient) { }

  getDepartamentos(): Observable<any> {
    return this.http.get(environment.endpointDepartamentos).pipe(
      map((data: Departamento[]) => data));
  }
}
