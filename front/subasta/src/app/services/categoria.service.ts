import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Categoria } from '../dtos/categoria';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  get(): Observable<any[]> {
    return this.http.get<any[]>(environment.endpointCategoria).pipe(
      map((data: Categoria[]) => data));
  }

  getDto(id): Observable<any> {
    return this.http.get<any>(environment.endpointCategoria + '/Get/' + id).pipe(
      map((data: any) => data));
  }

  post(dto: Categoria): Observable<any> {
    return this.http.post<any>(environment.endpointCategoria, dto, { headers: this.httpHeaders });
  }

  put(dto: Categoria): Observable<any> {
    return this.http.put<any>(environment.endpointCategoria, dto, { headers: this.httpHeaders });
  }

  delete(id: string): Observable<any> {
    return this.http.delete<any>(environment.endpointCategoria + '/' + id, { headers: this.httpHeaders });
  }
}
