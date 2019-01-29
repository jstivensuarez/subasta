import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UsuarioService } from '../usuario.service';
import { JwtHelperService } from '@auth0/angular-jwt';
@Injectable({
    providedIn: 'root'
})
export class AutenticacionService implements CanActivate {

    constructor(private usuarioService: UsuarioService) { }

    canActivate(): boolean {
        if (!this.usuarioService.isAuthenticated()) {
            this.usuarioService.logout();
            return false;
        }
        return true;
    }
}
