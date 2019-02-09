import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Lote } from '../dtos/lote';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LotesService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  get(): Observable<any[]> {
    return this.http.get<any[]>(environment.endpointLote).pipe(
      map((data: Lote[]) => data));
  }

  getDto(id): Observable<any> {
    return this.http.get<any>(environment.endpointLote + '/Get/' + id).pipe(
      map((data: any) => data));
  }

  getNoAsociados(clienteId): Observable<any> {
    return this.http.get<any>(environment.endpointLote + '/GetNoAsociados/' + clienteId).pipe(
      map((data: any) => data));
  }

  post(dto: FormData): Observable<any> {
    return this.http.post<any>(environment.endpointLote, dto);
  }

  put(dto: FormData): Observable<any> {
    return this.http.put<any>(environment.endpointLote, dto);
  }

  delete(id: string): Observable<any> {
    return this.http.delete<any>(environment.endpointLote + '/' + id, { headers: this.httpHeaders });
  }
}
