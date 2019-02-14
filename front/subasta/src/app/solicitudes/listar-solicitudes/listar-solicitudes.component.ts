import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatPaginator, MatTableDataSource } from '@angular/material';
import { Solicitud } from 'src/app/dtos/solicitud-subasta';
import { SubastaService } from 'src/app/services/subasta.service';
import { Router } from '@angular/router';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { SolicitudService } from 'src/app/services/solicitud.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { VerSolicitudComponent } from '../ver-solicitud/ver-solicitud.component';

@Component({
  selector: 'app-listar-solicitudes',
  templateUrl: './listar-solicitudes.component.html',
  styleUrls: ['./listar-solicitudes.component.css']
})
export class ListarSolicitudesComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: MatTableDataSource<Solicitud>;
  solicitudes: Solicitud[];
  displayedColumns: string[] = ['cliente', 'subasta', 'acciones'];
  title: string;
  constructor(
    private solicitudService: SolicitudService,
    private router: Router,
    private alertService: MesaggesManagerService,
    private modalService: NgbModal
  ) {
    this.solicitudes = [];
    this.dataSource = new MatTableDataSource(this.solicitudes);
    this.title = "Solicitudes";
    this.obtenerSolicitudes();
  }

  ngOnInit() {
  }

  obtenerSolicitudes() {
    this.solicitudService.get().subscribe(resp => {
      this.solicitudes = resp;
      this.dataSource = new MatTableDataSource(resp);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }, err => {
      console.error(err);
    });
  }

  verSolicitud(solicitud) {
    const component = this.modalService.open(VerSolicitudComponent).componentInstance;
    component.solicitud = solicitud;
    component.completo.subscribe(res => {
      this.obtenerSolicitudes();
    });
  }

  applyFilter(filtro: string) {
    const solicitudesFiltrados = this.solicitudes.filter(
      option =>
        option.subasta.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.subasta.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.cliente.nombre.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.cliente.nombre.toLowerCase().indexOf(filtro.toLowerCase()) > 0);
    this.dataSource.data = solicitudesFiltrados;
    this.dataSource.paginator.firstPage();
  }
}
