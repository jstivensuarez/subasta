import { Component, OnInit, ViewChild } from '@angular/core';
import { SubastaService } from 'src/app/services/subasta.service';
import { Subasta } from 'src/app/dtos/subasta';
import { constants } from 'src/app/util/constants';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { Router } from '@angular/router';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-listar-subasta',
  templateUrl: './listar-subasta.component.html',
  styleUrls: ['./listar-subasta.component.css']
})
export class ListarSubastaComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: MatTableDataSource<Subasta>;
  subastas: Subasta[];
  displayedColumns: string[] = ['descripcion', 'horaInicio', 'horaFin','evento','acciones'];
  titleSubastas: string;
  constructor(
    private subastaService: SubastaService,
    private router: Router,
    private alertService: MesaggesManagerService,
  ) { 
    this.titleSubastas = "Subastas";
    this.obtenerSubastas();
  }

  ngOnInit() {
  }


  eliminarSubasta(subasta) {
    this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDeleteSubasta).subscribe(
      resp => {
        if (resp) {
          this.subastaService.delete(subasta.subastaId).subscribe(
            resp => {
              this.alertService.
                showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
              this.obtenerSubastas();
            }, err => {
              this.alertService.
                showSimpleMessage(constants.errorTitle, constants.error, constants.errorDelete);
            }
          );
        }
      }
    );
  }

  editarSubasta(subasta) {
    this.router.navigate(['/crear-subasta'], { queryParams: { id: subasta.subastaId } });
  }

  agregarSubasta() {
    this.router.navigate(['/crear-subasta']);
  }
  
  obtenerSubastas() {
    this.subastaService.get().subscribe(resp => {
      this.subastas = resp;
      this.dataSource = new MatTableDataSource(resp);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }, err => {
      console.error(err);
    });
  }

  applyFilter(filtro: string) {
    const subastasFiltrados = this.subastas.filter(
      option => option.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.horaInicio.toString().toLowerCase().lastIndexOf(filtro.toLowerCase()) === 0 ||
        option.horaInicio.toString().toLowerCase().lastIndexOf(filtro.toLowerCase()) > 0 ||
        option.horaFin.toString().indexOf(filtro.toLowerCase()) === 0 ||
        option.horaFin.toString().indexOf(filtro.toLowerCase()) > 0 ||
        option.evento.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.evento.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) > 0);
    this.dataSource.data = subastasFiltrados;
    this.dataSource.paginator.firstPage();
  }
}
