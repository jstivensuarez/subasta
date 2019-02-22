import { Component, OnInit, ViewChild } from '@angular/core';
import { LotesService } from 'src/app/services/lotes.service';
import { Lote } from 'src/app/dtos/lote';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { constants } from 'src/app/util/constants';
import { ActivatedRoute, Router } from '@angular/router';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-listar-lotes',
  templateUrl: './listar-lotes.component.html',
  styleUrls: ['./listar-lotes.component.css']
})
export class ListarLotesComponent implements OnInit {
  lotes: Lote[];
  displayedColumns: string[] = ['nombre', 'cantidadElementos', 'precioBase', 'municipio', 'subasta', 'acciones'];
  title: string;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: MatTableDataSource<Lote>;

  constructor(private lotesService: LotesService,
    private alertService: MesaggesManagerService,
    private route: ActivatedRoute,
    private router: Router) {
    this.title = "Lotes";
    this.obtenerLotes();
  }

  ngOnInit() {
  }

  eliminar(lote) {
    this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDelete).subscribe(
      resp => {
        if (resp) {
          this.lotesService.delete(lote.loteId).subscribe(
            resp => {
              this.alertService.
                showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
              this.obtenerLotes();
            }, err => {
              this.alertService.
                showSimpleMessage(constants.errorTitle, constants.error, constants.errorDelete);
            }
          );
        }
      }
    );
  }

  ver(lote) {
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
      "Valor de anticipo": lote.valorAnticipo,
      imagen: imagen,
      video: video,
      Propietario: lote.cliente.nombre,
      Ciudad: lote.municipio.descripcion,
      Subasta: lote.subasta.descripcion,
      Categoría: lote.categoria.descripcion,
      Raza: lote.raza.descripcion,
      Clasificación: lote.clasificacion.descripcion
    });
  }

  editar(lote) {
    this.router.navigate(['/crear-lote'], { queryParams: { id: lote.loteId } });
  }

  obtenerLotes() {
    this.lotesService.get().subscribe(
      resp => {
        this.lotes = resp;
        this.dataSource = new MatTableDataSource(resp);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }, err => {
        console.error(err);
      });
  }

  agregarLote() {
    this.router.navigate(['/crear-lote']);
  }

  getImage(nombre) {
    const urlImages = environment.imageLotesUrl;
    return urlImages + nombre;
  }

  applyFilter(filtro: string) {
    const lotesFiltrados = this.lotes.filter(
      option => option.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.cantidadElementos.toString().toLowerCase().lastIndexOf(filtro.toLowerCase()) === 0 ||
        option.cantidadElementos.toString().toLowerCase().lastIndexOf(filtro.toLowerCase()) > 0 ||
        option.precioBase.toString().toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.precioBase.toString().toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.municipio.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.municipio.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.nombre.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.nombre.toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.subasta.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.subasta.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) > 0);
    this.dataSource.data = lotesFiltrados;
    this.dataSource.paginator.firstPage();
  }

}
