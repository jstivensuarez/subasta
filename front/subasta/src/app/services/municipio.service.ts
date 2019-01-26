import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Municipio } from '../dtos/municipio';

@Injectable({
  providedIn: 'root'
})
export class MunicipioService {

  constructor(private http: HttpClient) { }

  getMunicipios(): Observable<any> {
    return this.http.get(environment.endpointMunicipios).pipe(
      map((data: Municipio[]) => data));
  }
}
