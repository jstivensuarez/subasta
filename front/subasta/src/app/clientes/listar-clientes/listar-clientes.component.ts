import { Component, OnInit } from '@angular/core';
import { ClienteService } from 'src/app/services/cliente.service';
import { Cliente } from 'src/app/dtos/cliente';
import { ActivatedRoute, Router } from '@angular/router';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { constants } from 'src/app/util/constants';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MatDialog } from '@angular/material';
import { MunicipioService } from 'src/app/services/municipio.service';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

@Component({
  selector: 'app-listar-clientes',
  templateUrl: './listar-clientes.component.html',
  styleUrls: ['./listar-clientes.component.css']
})
export class ListarClientesComponent implements OnInit {
  title = 'Clientes';
  displayedColumns: string[] = ['nombre', 'correo', 'telefono', 'usuario', 'acciones'];
  clientes: Cliente[];
  constructor(private clienteService: ClienteService,
    private router: Router,
    private alertService: MesaggesManagerService,
    private municipioService: MunicipioService) {
    this.clientes = [];
    this.obtenerClientes();
  }

  ngOnInit() {
  }

  editar(cliente) {
    this.router.navigate(['/crear-propietario'], { queryParams: { id: cliente.clienteId } });
  }

  eliminar(cliente) {
    debugger;
    this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDelete).subscribe(
      resp => {
        if (resp) {
          this.clienteService.delete(cliente.clienteId).subscribe(
            resp => {
              debugger;
              this.alertService.
                showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
              this.obtenerClientes();
            }, err => {
              debugger;
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
        Ubicación: resp.direccion + ' (' + resp.municipio.descripcion+')',
        Documento: resp.clienteId+ ' ('+ resp.tipoDocumento.descripcion+')',
        Representante: resp.representante
      });
    }, err => {
      console.error(err);
    });
   
  }

  obtenerClientes() {
    this.clienteService.get().subscribe(
      resp => {
        this.clientes = resp;
      }, err => {
        console.error(err);
      });
  }
}

