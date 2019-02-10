import { Component, OnInit } from '@angular/core';
import { UsuarioService } from '../services/usuario.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  claims: any;
  usuario: string;
  constructor(private usuarioService: UsuarioService) { 
    this.usuario = '';
    this.getClaims();
  }

  ngOnInit() {
  }

  getClaims() {
    if (this.usuarioService.isAuthenticated()) {
      this.claims = this.usuarioService.getClaims();
      this.usuario = this.claims.sub;
    }
  }

  logout() {
    this.usuarioService.logout();
  }
}
