import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Municipio } from '../dtos/municipio';

@Injectable({
  providedIn: 'root'
})
export class MunicipioService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
  
  constructor(private http: HttpClient) { }

  getMunicipios(id): Observable<any> {
    return this.http.get(environment.endpointMunicipios+'/GetPorDepartamento/'+ id).pipe(
      map((data: Municipio[]) => data));
  }

  getDto(id): Observable<any> {
    return this.http.get<any>(environment.endpointMunicipios + '/Get/' + id).pipe(
      map((data: any) => data));
  }

  post(dto: Municipio): Observable<any> {
    return this.http.post<any>(environment.endpointMunicipios, dto, { headers: this.httpHeaders });
  }

  put(dto: Municipio): Observable<any> {
    return this.http.put<any>(environment.endpointMunicipios, dto, { headers: this.httpHeaders });
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(environment.endpointMunicipios + '/' + id, { headers: this.httpHeaders });
  }
}
