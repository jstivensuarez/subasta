import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Pujador } from '../dtos/pujador';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PujadoresService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  get(): Observable<any[]> {
    return this.http.get<any[]>(environment.endpointPujador).pipe(
      map((data: Pujador[]) => data));
  }

  getDto(id): Observable<any> {
    return this.http.get<any>(environment.endpointPujador + '/Get/' + id).pipe(
      map((data: any) => data));
  }

  post(dto: Pujador): Observable<any> {
    return this.http.post<any>(environment.endpointPujador, dto, { headers: this.httpHeaders });
  }

  put(dto: Pujador): Observable<any> {
    return this.http.put<any>(environment.endpointPujador, dto, { headers: this.httpHeaders });
  }

  delete(id: string): Observable<any> {
    return this.http.delete<any>(environment.endpointPujador + '/' + id, { headers: this.httpHeaders });
  }
}
