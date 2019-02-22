import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Pujador } from 'src/app/dtos/pujador';
import { PujadoresService } from 'src/app/services/pujadores.service';
import { Router } from '@angular/router';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { constants } from 'src/app/util/constants';

@Component({
  selector: 'app-listar',
  templateUrl: './listar.component.html',
  styleUrls: ['./listar.component.css']
})
export class ListarPujadorComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: MatTableDataSource<Pujador>;
  title = 'Pujadores';
  displayedColumns: string[] = ['cliente', 'lote','banco','numeroConsignacion', 'valorConsignacion','acciones'];
  pujadores: Pujador[];
  constructor(private pujadorService: PujadoresService,
    private router: Router,
    private alertService: MesaggesManagerService) {
    this.pujadores = [];
    this.obtenerPujadores();
  }

  ngOnInit() {
  }

  agregarPujador() {
    this.router.navigate(['/crear-pujador']);
  }

  editar(pujador) {
    this.router.navigate(['/crear-pujador'],
     { queryParams: { id: pujador.pujadorId }});
  }

  eliminar(cliente) {
    this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDelete).subscribe(
      resp => {
        if (resp) {
          this.pujadorService.delete(cliente.pujadorId).subscribe(
            resp => {
              this.alertService.
                showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
              this.obtenerPujadores();
            }, err => {
              this.alertService.
                showSimpleMessage(constants.errorTitle, constants.error, constants.errorDelete);
            }
          );
        }
      }
    );
  }

  ver(pujador) {
    this.pujadorService.getDto(pujador.pujadorId).subscribe(resp => {
      this.alertService.showDetails('Detalles del pujador', {

      });
    }, err => {
      console.error(err);
    });

  }

  obtenerPujadores() {
    this.pujadorService.get().subscribe(
      resp => {
        this.pujadores = resp;
        this.dataSource = new MatTableDataSource(resp);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }, err => {
        console.error(err);
      });
  }

  applyFilter(filtro: string) {
    const pujadoresFiltrados = this.pujadores.filter(
      option => option.banco.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.banco.toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.cliente.nombre.toLowerCase().lastIndexOf(filtro.toLowerCase()) === 0 ||
        option.cliente.nombre.toLowerCase().lastIndexOf(filtro.toLowerCase()) > 0 ||
        option.lote.nombre.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.lote.nombre.toLowerCase().indexOf(filtro.toLowerCase()) > 0);
    this.dataSource.data = pujadoresFiltrados;
    this.dataSource.paginator.firstPage();
  }
}
