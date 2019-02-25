import { Component, OnInit } from '@angular/core';
import { Title, DomSanitizer } from '@angular/platform-browser';
import { EventoService } from 'src/app/services/evento.service';
import { Evento } from 'src/app/dtos/evento';
import { UsuarioService } from 'src/app/services/usuario.service';
import { environment } from 'src/environments/environment';
import { constants } from 'src/app/util/constants';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { SolicitudService } from 'src/app/services/solicitud.service';
import { Solicitud } from 'src/app/dtos/solicitud-subasta';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PujarComponent } from '../pujar/pujar.component';
import { FormControl, Validators } from '@angular/forms';
import { SignalRService } from 'src/app/services/signal-r.service';

@Component({
  selector: 'app-subastas',
  templateUrl: './subastas.component.html',
  styleUrls: ['./subastas.component.css']
})
export class SubastasComponent implements OnInit {

  estaAutenticado: boolean;
  eventos: Evento[];
  title: string;
  isAdmin: boolean;
  usuario: string;
  formatoFecha: any = {
    Days: " Días:",
    Hours: " Horas:",
    Minutes: " Minutos:",
    Seconds: " Segundos",
  };
  constructor(private eventoService: EventoService,
    private _sanitizer: DomSanitizer,
    private usuarioService: UsuarioService,
    private alertService: MesaggesManagerService,
    private solicitudService: SolicitudService,
    private modalService: NgbModal,
    private signalRService: SignalRService) {
    this.isAdmin = false;
    this.title = "Subastas";
    this.eventos = [];
    this.obtenerEventos();
  }

  ngOnInit() {
    this.signalRService.nuevoMensaje.subscribe(mensaje => {
      const objeto = JSON.parse(mensaje);
      if (objeto.Tipo == "ACTUALIZAR_LOTE_PUJA") {
        const puja = JSON.parse(objeto.Mensaje);
        const evento = this.eventos.find(e => e.eventoId == puja.eventoId);
        const subasta = evento.subastasDto.find(s => s.subastaId == puja.subastaId);
        const lote = subasta.lotesDto.find(l => l.loteId == puja.loteId);
        if (lote.pujaMinima.usuario == this.usuario && lote.pujaMinima.usuario != puja.usuario) {
          this.alertService.
            showSimpleMessage(constants.nuevaPujaTitle, constants.nuevaPuja, constants.pujaSuperada+ "'" +lote.nombre+ "' ha sido superada");
        }
        lote.pujaMinima.usuario = puja.usuario;
        lote.pujaMinima.valor = puja.valor;
      }
    });
    this.signalRService.IniciarConeccion();
  }

  obtenerEventos() {
    this.estaAutenticado = this.usuarioService.isAuthenticated();
    if (this.estaAutenticado) {
      this.usuario = this.obtenerUsurio();
      this.obtenerParaClienteAutenticado();
    } else {
      this.obtenerParaClientes();
    }
  }

  obtenerParaClienteAutenticado() {
    this.eventoService.getForClientAutenticated().subscribe(resp => {
      this.eventos = resp;
      this.isAdmin = this.EsAdministrador();
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

  validarFinal(fechaFin) {
    const hoy = new Date();
    if (hoy <= new Date(fechaFin)) {
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

  enviarSolicitud(subasta) {
    this.alertService.showConfirmMessage(constants.solicitudTitle, constants.confirmSolicitud).subscribe(
      resp => {
        if (resp) {
          const solicitud = new Solicitud();
          solicitud.subastaId = subasta.subastaId;
          this.solicitudService.post(solicitud).subscribe(
            resp => {
              subasta.estadoSolicitud = 'PENDIENTE_POR_APROBAR';
              console.log(resp)
            }, err => {
              console.error(err);
            }
          );
        }
      }
    );
  }

  obtenerUsurio() {
    if (this.usuarioService.isAuthenticated()) {
      const claims = this.usuarioService.getClaims();
      return claims.sub;
    }
    return null;
  }

  EsAdministrador() {
    if (this.usuarioService.isAuthenticated()) {
      const claims = this.usuarioService.getClaims();
      if (claims && claims.Role.toLowerCase() == 'administrador') {
        return true;
      }
    }
    return false;
  }

  Pujar(lote) {
    const component = this.modalService.open(PujarComponent).componentInstance;
    component.min = lote.pujaMinima.valor + 1;
    component.loteId = lote.loteId;
    component.usuario = this.usuario;
    component.control = new FormControl(lote.pujaMinima.valor + 1, [Validators.min(lote.pujaMinima.valor + 1)]);
    component.completo.subscribe(resp => {
      lote.pujaMinima.usuario = this.usuario;
      lote.pujaMinima.valor = resp;
      this.obtenerEventos();
    });
  }

  verLote(lote) {
    setTimeout(function () {
      let video = null;
      let imagen = null;
      if (/^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$/.test(lote.fotoLote)) {
        video = lote.fotoLote;
      } else {
        imagen = 'LOTES/' + lote.fotoLote;
      }
      this.alertService.showDetails('Detalles del lote', {
        Nombre: lote.nombre,
        "Cantidad de animales": lote.cantidadElementos,
        "Peso Total": lote.pesoTotal,
        "Promedio": lote.pesoPromedio,
        "Precio base": lote.precioBase,
        imagen: imagen,
        video: video,
        Ciudad: lote.municipio.descripcion,
        Subasta: lote.subasta.descripcion,
        Categoría: lote.categoria.descripcion,
        Raza: lote.raza.descripcion,
        Clasificación: lote.clasificacion.descripcion
      });
    }.bind(this), 1500);
  }
}
