import { Component, OnInit } from '@angular/core';
import { Evento } from 'src/app/dtos/evento';
import { EventoService } from 'src/app/services/evento.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-listar-eventos',
  templateUrl: './listar-eventos.component.html',
  styleUrls: ['./listar-eventos.component.css']
})
export class ListarEventosComponent implements OnInit {

  eventos: Evento[];
  isEditing: boolean;
  displayedColumns: string[] = ['descripcion', 'fechaInicio', 'fechaFin', 'municipio', 'acciones'];
  
  constructor(private eventoService: EventoService,
    private router: Router,
    private alertService: MesaggesManagerService,
    private route: ActivatedRoute) {
    this.obtenerEventos();
   }

  ngOnInit() {
  }

  editar(evento) {
    this.router.navigate(['/crear-evento'], { queryParams: { id: evento.eventoId } });
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
