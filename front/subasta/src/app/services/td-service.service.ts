import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { TipoDocumento } from '../dtos/tipo-documento';

@Injectable({
  providedIn: 'root'
})
export class TdServiceService {

  constructor(private http: HttpClient) { }

  getTipoDocumentos(): Observable<any> {
    return this.http.get(environment.endpointTipoDocumentos).pipe(
      map((data: TipoDocumento[]) => data));
  }
}
