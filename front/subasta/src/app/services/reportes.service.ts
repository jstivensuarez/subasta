import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/internal/operators/map';

@Injectable({
  providedIn: 'root'
})
export class ReportesService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getLotesVendidos(eventoId): Observable<any> {
    return this.http.get(environment.endpointReportes + '/GetLotesVendidos/'
      + eventoId, {
        responseType: 'blob'
      });
  }

  getCompradoresPorLote(eventoId): Observable<any> {
    return this.http.get(environment.endpointReportes + '/GetCompradoresPorLote/'
      + eventoId, {
        responseType: 'blob'
      });
  }
}
