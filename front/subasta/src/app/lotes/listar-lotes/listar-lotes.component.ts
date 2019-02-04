import { Component, OnInit } from '@angular/core';
import { LotesService } from 'src/app/services/lotes.service';
import { Lote } from 'src/app/dtos/lote';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { constants } from 'src/app/util/constants';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-listar-lotes',
  templateUrl: './listar-lotes.component.html',
  styleUrls: ['./listar-lotes.component.css']
})
export class ListarLotesComponent implements OnInit {
  lotes: Lote[];
  displayedColumns: string[] = ['nombre', 'cantidadElementos', 'precioBase', 'municipio', 'subasta', 'acciones'];
  title: string;
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
    if(/^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$/.test(lote.fotoLote)){
      video = lote.fotoLote;
    }else{
      imagen = lote.fotoLote;
    }
    this.alertService.showDetails('Detalles del lote', {
      Nombre: lote.nombre,
      Descripción: lote.descripcion,
      "Cantidad de elementos": lote.cantidadElementos,
      "Peso Total": lote.pesoTotal,
      "Precio base": lote.precioBase,
      "Valor de anticipo": lote.valorAnticipo,
      imagen: imagen,
      video: video,
      Propietario: lote.cliente.nombre,
      Ciudad: lote.municipio.descripcion,
      Subasta: lote.subasta.descripcion
    });
  }

  editar(lote) {
    this.router.navigate(['/crear-lote'], { queryParams: { id: lote.loteId } });
  }

  obtenerLotes() {
    this.lotesService.get().subscribe(
      resp => {
        this.lotes = resp;
      }, err => {
        console.error(err);
      });
  }

  agregarLote() {
    this.router.navigate(['/crear-lote']);
  }

  getImage(nombre) {
    const urlImages = 'http://localhost:50553/images/LOTES/';
    return urlImages + nombre;
  }
}
