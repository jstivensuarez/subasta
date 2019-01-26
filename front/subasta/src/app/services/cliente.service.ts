import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Cliente } from '../dtos/cliente';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  get(): Observable<any[]> {
    return this.http.get<any[]>(environment.endpointClientes).pipe(
      map((data: Cliente[]) => data));
  }

  getCliente(id): Observable<any> {
    return this.http.get<any>(environment.endpointClientes + '/Get/' + id).pipe(
      map((data: any) => data));
  }

  post(cliente: Cliente): Observable<any> {
    return this.http.post<any>(environment.endpointClientes, cliente, { headers: this.httpHeaders });
  }

  put(cliente: Cliente): Observable<any> {
    return this.http.put<any>(environment.endpointClientes, cliente, { headers: this.httpHeaders });
  }

  delete(userId: string): Observable<any> {
    return this.http.delete<any>(environment.endpointClientes + '/' + userId, { headers: this.httpHeaders });
  }
}
