import { Injectable } from '@angular/core';
import { Usuario } from '../dtos/usuario';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import decode from 'jwt-decode';
import { Cliente } from '../dtos/cliente';
@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient,
    private router: Router,
    public jwtHelper: JwtHelperService) { }

  login(dto: Usuario): Observable<any> {
    return this.http.post<any>(environment.endpointLogin+'/login', dto, { headers: this.httpHeaders });
  }

  register(dto: Cliente): Observable<any> {
    return this.http.post<any>(environment.endpointLogin+'/register', dto, { headers: this.httpHeaders });
  }

  validate(nombreUsuario: string): Observable<any> {
    return this.http.post<any>(environment.endpointLogin+'/validateUser/'+nombreUsuario, { headers: this.httpHeaders });
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    window.location.reload();
    localStorage.removeItem('token');
    setTimeout(function() {
      this.router.navigate(['login']);
    }, 2500);

  }

  getClaims() {
    const token = localStorage.getItem('token');
    const tokenPayload = decode(token);
    return tokenPayload;
  }

  redirectToMenu() {
    const claims = this.getClaims();
    window.location.reload();
    if(claims.Role == 'Administrador'){
      this.router.navigate(['listar-evento']);
    }
    if(claims.Role == 'Pujador'){
       this.router.navigate(['subastas']);
    }
  }
}
