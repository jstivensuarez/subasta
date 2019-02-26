import { Injectable } from '@angular/core';
import { Usuario } from '../dtos/usuario';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import decode from 'jwt-decode';
import { Cliente } from '../dtos/cliente';
import { map } from 'rxjs/internal/operators/map';
import { constants } from '../util/constants';
import { MesaggesManagerService } from './mesagges-manager.service';
@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
  claims = new Subject<any>();

  constructor(private http: HttpClient,
    private router: Router,
    public jwtHelper: JwtHelperService,
    private alertService: MesaggesManagerService) { }



  login(dto: Usuario): Observable<any> {
    return this.http.post<any>(environment.endpointLogin + '/login', dto, { headers: this.httpHeaders });
  }

  register(dto: Cliente): Observable<any> {
    return this.http.post<any>(environment.endpointLogin + '/register', dto, { headers: this.httpHeaders });
  }

  validate(nombreUsuario: string): Observable<any> {
    return this.http.post<any>(environment.endpointLogin + '/validateUser/' + nombreUsuario, { headers: this.httpHeaders });
  }

  recover(nombreUsuario: string): Observable<any> {
    return this.http.post<any>(environment.endpointLogin + '/RecoverePass/' + nombreUsuario, { headers: this.httpHeaders });
  }

  change(usuario: Usuario): Observable<any> {
    return this.http.post<any>(environment.endpointLogin + '/ChangePass', usuario, { headers: this.httpHeaders });
  }

  get(): Observable<any[]> {
    return this.http.get<any[]>(environment.endpointUsuario).pipe(
      map((data: Usuario[]) => data));
  }

  getDto(id): Observable<any> {
    return this.http.get<any>(environment.endpointUsuario + '/Get/' + id).pipe(
      map((data: any) => data));
  }

  post(dto: Usuario): Observable<any> {
    return this.http.post<any>(environment.endpointUsuario, dto, { headers: this.httpHeaders });
  }

  put(dto: Usuario): Observable<any> {
    return this.http.put<any>(environment.endpointUsuario, dto, { headers: this.httpHeaders });
  }

  delete(id: string): Observable<any> {
    return this.http.delete<any>(environment.endpointUsuario + '/' + id, { headers: this.httpHeaders });
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    this.deleteClaimsAndToken();
    this.router.navigate(['/login']);
  }

  logoutSession() {
    this.alertService.
      showSimpleMessage(constants.sessionTitle, constants.alert, constants.sessionExpiradaa);
    this.deleteClaimsAndToken();
    this.router.navigate(['/login']);
  }

  getClaims() {
    const token = localStorage.getItem('token');
    const tokenPayload = decode(token);
    return tokenPayload;
  }

  redirectToMenu() {
    const claims = this.getClaims();
    this.claims.next(claims);
    this.router.navigate(['subastas']);
  }

  deleteClaimsAndToken() {
    localStorage.removeItem("token");
    this.claims.next(null);
  }
}
