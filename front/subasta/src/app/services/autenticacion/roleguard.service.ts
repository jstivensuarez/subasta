import { Injectable } from '@angular/core';
import { UsuarioService } from '../usuario.service';
import { ActivatedRouteSnapshot, CanActivate } from '@angular/router';
import decode from 'jwt-decode';
@Injectable({
  providedIn: 'root'
})
export class RoleguardService implements CanActivate {

  constructor(public usuarioService: UsuarioService) { }
  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRole = route.data.expectedRole;
    const token = localStorage.getItem('token');
    // decode the token to get its payload
    const tokenPayload = decode(token);
    if (
      !this.usuarioService.isAuthenticated() ||
      tokenPayload.Role !== expectedRole
    ) {
      this.usuarioService.logout();
      return false;
    }
    return true;
  }
}
