import { Component, OnInit, ViewChild } from '@angular/core';
import { ClienteService } from 'src/app/services/cliente.service';
import { Cliente } from 'src/app/dtos/cliente';
import { ActivatedRoute, Router } from '@angular/router';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { constants } from 'src/app/util/constants';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MatDialog, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { MunicipioService } from 'src/app/services/municipio.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-listar-propietarios',
  templateUrl: './listar-propietarios.component.html',
  styleUrls: ['./listar-propietarios.component.css']
})
export class ListarPropietariosComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: MatTableDataSource<Cliente>;
  title = 'Propietarios';
  displayedColumns: string[] = ['nombre', 'correo', 'telefono', 'acciones'];
  clientes: Cliente[];
  constructor(private clienteService: ClienteService,
    private router: Router,
    private alertService: MesaggesManagerService,
    private municipioService: MunicipioService,
    private _sanitizer: DomSanitizer) {
    this.clientes = [];
    this.obtenerClientes();
  }

  ngOnInit() {
  }

  editar(cliente) {
    this.router.navigate(['/crear-propietario'], { queryParams: { id: cliente.clienteId } });
  }

  eliminar(cliente) {
    this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDelete).subscribe(
      resp => {
        if (resp) {
          this.clienteService.delete(cliente.clienteId).subscribe(
            resp => {
              this.alertService.
                showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
              this.obtenerClientes();
            }, err => {
              this.alertService.
                showSimpleMessage(constants.errorTitle, constants.error, constants.errorDelete);
            }
          );
        }
      }
    );
  }

  ver(cliente) {
    this.clienteService.getDto(cliente.clienteId).subscribe(resp => {
      this.alertService.showDetails('Detalles del cliente', {
        Nombre: resp.nombre,
        Correo: resp.correo,
        Usuario: resp.usuario,
        Teléfono: resp.telefono,
        Ubicación: resp.direccion + ' (' + resp.municipio.descripcion + ')',
        Documento: resp.clienteId + ' (' + resp.tipoDocumento.descripcion + ')',
        Representante: resp.representante
      });
    }, err => {
      console.error(err);
    });

  }

  agregarPropietario() {
    this.router.navigate(['/crear-propietario']);
  }
  
  obtenerClientes() {
    this.clienteService.getPropietarios().subscribe(
      resp => {
        this.clientes = resp;
        this.dataSource = new MatTableDataSource(resp);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }, err => {
        console.error(err);
      });
  }

  applyFilter(filtro: string) {
    const clientesFiltrados = this.clientes.filter(
      option => option.correo.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.correo.toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.nombre.toString().toLowerCase().lastIndexOf(filtro.toLowerCase()) === 0 ||
        option.nombre.toString().toLowerCase().lastIndexOf(filtro.toLowerCase()) > 0 ||
        option.telefono.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.telefono.toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.tipoDocumento.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.tipoDocumento.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.municipio.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.municipio.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) > 0);
    this.dataSource.data = clientesFiltrados;
    this.dataSource.paginator.firstPage();
  }
}

