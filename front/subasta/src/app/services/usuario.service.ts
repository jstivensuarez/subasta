import { Injectable } from '@angular/core';
import { Usuario } from '../dtos/usuario';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import decode from 'jwt-decode';
@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient,
    private router: Router,
    public jwtHelper: JwtHelperService) { }

  login(dto: Usuario): Observable<any> {
    return this.http.post<any>(environment.endpointLogin, dto, { headers: this.httpHeaders });
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    window.location.reload();
    localStorage.removeItem('token');
    this.router.navigate(['login']);
  }

  getClaims() {
    const token = localStorage.getItem('token');
    const tokenPayload = decode(token);
    return tokenPayload
  }

  redirectToMenu(){
    window.location.reload();
    this.router.navigate(['/listar-lote']);
  }
}
