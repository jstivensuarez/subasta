import { Component, OnInit } from '@angular/core';
import { UsuarioService } from '../services/usuario.service';
import { SolicitudService } from '../services/solicitud.service';
import { Solicitud } from '../dtos/solicitud-subasta';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  claims: any;
  usuario: string;
  cantidadSolicitudes: number;
  constructor(private usuarioService: UsuarioService,
    private solicitudService: SolicitudService) {
    this.cantidadSolicitudes = 0;
    this.usuario = '';
    this.obtenerUsuario();
  }

  ngOnInit() {
    this.usuarioService.claims.subscribe( claims => {
      this.claims = claims;
      this.usuario = claims.sub;
    });
  }

  obtenerUsuario() {
    if (this.usuarioService.isAuthenticated()) {
      this.claims = this.usuarioService.getClaims();
      this.usuario = this.claims.sub;
    }
  }

  logout() {
    this.usuarioService.logout();
  }
}
