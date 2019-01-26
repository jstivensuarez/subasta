import { Component, OnInit } from '@angular/core';
import { ClienteService } from 'src/app/services/cliente.service';
import { Cliente } from 'src/app/dtos/cliente';
import { ActivatedRoute, Router } from '@angular/router';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { constants } from 'src/app/util/constants';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MatDialog } from '@angular/material';

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
    private mesaggesManagerService: MesaggesManagerService) {
    this.clientes = [];
    this.obtenerClientes();
  }

  ngOnInit() {
  }

  editar(cliente) {
    this.router.navigate(['/crear-propietario'], { queryParams: { id: cliente.clienteId } });
  }

  eliminar(cliente) {
    alert(cliente.nombre);
  }

  ver(cliente) {
    alert(cliente.nombre);
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

