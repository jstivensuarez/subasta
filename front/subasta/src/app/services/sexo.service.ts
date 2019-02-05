import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Sexo } from '../dtos/sexo';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SexoService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  get(): Observable<any[]> {
    return this.http.get<any[]>(environment.endpointSexo).pipe(
      map((data: Sexo[]) => data));
  }

  getDto(id): Observable<any> {
    return this.http.get<any>(environment.endpointSexo + '/Get/' + id).pipe(
      map((data: any) => data));
  }

  post(dto: Sexo): Observable<any> {
    return this.http.post<any>(environment.endpointSexo, dto, { headers: this.httpHeaders });
  }

  put(dto: Sexo): Observable<any> {
    return this.http.put<any>(environment.endpointSexo, dto, { headers: this.httpHeaders });
  }

  delete(id: string): Observable<any> {
    return this.http.delete<any>(environment.endpointSexo + '/' + id, { headers: this.httpHeaders });
  }
}
