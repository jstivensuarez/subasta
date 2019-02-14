import { Component, OnInit } from '@angular/core';
import { Title, DomSanitizer } from '@angular/platform-browser';
import { EventoService } from 'src/app/services/evento.service';
import { Evento } from 'src/app/dtos/evento';
import { UsuarioService } from 'src/app/services/usuario.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-subastas',
  templateUrl: './subastas.component.html',
  styleUrls: ['./subastas.component.css']
})
export class SubastasComponent implements OnInit {

  estaAutenticado: boolean;
  eventos: Evento[];
  title: string;
  constructor(private eventoService: EventoService,
    private _sanitizer: DomSanitizer,
    private usuarioService: UsuarioService) {
    this.title = "Subastas";
    this.eventos = [];
    this.obtenerEventos();
  }

  ngOnInit() {

  }

  obtenerEventos() {
    debugger;
    this.estaAutenticado = this.usuarioService.isAuthenticated();
    if (this.estaAutenticado) {
      this.obtenerParaClientes();
    } else {
      this.obtenerParaClientes();
    }
  }

  obtenerParaClienteAutenticado() {
    this.eventoService.getForClients().subscribe(resp => {
      this.eventos = resp;
    }, err => {
      console.error(err);
    });
  }

  obtenerParaClientes() {
    this.eventoService.getForClients().subscribe(resp => {
      this.eventos = resp;
    }, err => {
      console.error(err);
    });
  }

  validarInicio(fechaInicio) {
    const hoy = new Date();
    if (hoy >= new Date(fechaInicio)) {
      return true;
    }
    return false;
  }

  getVideo(video) {
    const url =
      this._sanitizer.bypassSecurityTrustResourceUrl(video.replace('watch?v=', 'embed/'));
    return url;
  }

  getImage(imagen) {
    const url = environment.imageLotesUrl + imagen;
    return url;
  }

  esVideo(imagen) {
    if (/^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$/.test(imagen)) {
      return true;
    }
    return false;
  }
}
