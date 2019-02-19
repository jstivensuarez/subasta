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
  formatoFecha: any = {
    Days: "Días ",
    Hours: "Horas",
    Minutes: "Min",
    Seconds: "Seg",
  };
  constructor(private eventoService: EventoService,
    private _sanitizer: DomSanitizer,
    private usuarioService: UsuarioService,
    private alertService: MesaggesManagerService,
    private solicitudService: SolicitudService,
    private modalService: NgbModal) {
    this.isAdmin = false;
    this.title = "Subastas";
    this.eventos = [];
    this.obtenerEventos();
  }

  ngOnInit() {

  }

  obtenerEventos() {
    this.estaAutenticado = this.usuarioService.isAuthenticated();
    if (this.estaAutenticado) {
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

  enviarSolicitud(subastaId) {
    this.alertService.showConfirmMessage(constants.solicitudTitle, constants.confirmSolicitud).subscribe(
      resp => {
        if (resp) {
          const solicitud = new Solicitud();
          solicitud.subastaId = subastaId;
          this.solicitudService.post(solicitud).subscribe(
            resp => {
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
      if(claims && claims.Role.toLowerCase() == 'administrador'){
        return true;
      }
    }
    return false;
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
