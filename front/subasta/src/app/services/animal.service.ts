import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Animal } from '../dtos/animal';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AnimalService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  get(): Observable<any[]> {
    return this.http.get<any[]>(environment.endpointAnimal).pipe(
      map((data: Animal[]) => data));
  }

  getDto(id): Observable<any> {
    return this.http.get<any>(environment.endpointAnimal + '/Get/' + id).pipe(
      map((data: any) => data));
  }

  post(dto: FormData): Observable<any> {
    return this.http.post<any>(environment.endpointAnimal, dto);
  }

  put(dto: FormData): Observable<any> {
    return this.http.put<any>(environment.endpointAnimal, dto);
  }

  delete(id: string): Observable<any> {
    return this.http.delete<any>(environment.endpointAnimal + '/' + id, { headers: this.httpHeaders });
  }
}
