import { Component, OnInit } from '@angular/core';
import { Evento } from 'src/app/dtos/evento';
import { EventoService } from 'src/app/services/evento.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { Router, ActivatedRoute } from '@angular/router';
import { constants } from 'src/app/util/constants';

@Component({
  selector: 'app-listar-eventos',
  templateUrl: './listar-eventos.component.html',
  styleUrls: ['./listar-eventos.component.css']
})
export class ListarEventosComponent implements OnInit {

  eventos: Evento[];
  isEditing: boolean;
  displayedColumns: string[] = ['descripcion', 'fechaInicio', 'fechaFin', 'municipio', 'acciones'];
  title: string;
  constructor(private eventoService: EventoService,
    private router: Router,
    private alertService: MesaggesManagerService,
    private route: ActivatedRoute) {
   this.title = "Eventos";
    this.obtenerEventos();
  }

  ngOnInit() {
  }

  editar(evento) {
    this.router.navigate(['/crear-evento'], { queryParams: { id: evento.eventoId } });
  }

  eliminar(evento) {
    this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDelete).subscribe(
      resp => {
        if (resp) {
          this.eventoService.delete(evento.eventoId).subscribe(
            resp => {
              this.alertService.
                showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
              this.obtenerEventos();
            }, err => {
              this.alertService.
                showSimpleMessage(constants.errorTitle, constants.error, constants.errorDelete);
            }
          );
        }
      }
    );
  }

  agregarEvento() {
    this.router.navigate(['/crear-evento']);
  }

  obtenerEventos() {
    this.eventoService.get().subscribe(
      resp => {
        debugger;
        this.eventos = resp;
      }, err => {
        console.error(err);
      });
  }
}
