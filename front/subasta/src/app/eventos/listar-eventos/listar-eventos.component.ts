import { Component, OnInit, ViewChild } from '@angular/core';
import { Evento } from 'src/app/dtos/evento';
import { EventoService } from 'src/app/services/evento.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { Router, ActivatedRoute } from '@angular/router';
import { constants } from 'src/app/util/constants';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-listar-eventos',
  templateUrl: './listar-eventos.component.html',
  styleUrls: ['./listar-eventos.component.css']
})
export class ListarEventosComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: MatTableDataSource<Evento>;
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
        this.eventos = resp;
        this.dataSource = new MatTableDataSource(resp);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }, err => {
        console.error(err);
      });
  }

  applyFilter(filtro: string) {
    const eventosFiltrados = this.eventos.filter(
      option => option.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.fechaFin.toString().toLowerCase().lastIndexOf(filtro.toLowerCase()) === 0 ||
        option.fechaFin.toString().toLowerCase().lastIndexOf(filtro.toLowerCase()) > 0 ||
        option.fechaInicio.toString().toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.fechaInicio.toString().toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.municipio.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.municipio.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) > 0);
    this.dataSource.data = eventosFiltrados;
    this.dataSource.paginator.firstPage();
  }
}
