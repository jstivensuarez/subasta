import { Component, OnInit } from '@angular/core';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { Raza } from 'src/app/dtos/raza';
import { Categoria } from 'src/app/dtos/categoria';
import { FormControl } from '@angular/forms';
import { ClasificacionService } from 'src/app/services/clasificacion.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CategoriaService } from 'src/app/services/categoria.service';
import { EditarClasificacionComponent } from '../editar-clasificacion/editar-clasificacion.component';
import { Clasificacion } from 'src/app/dtos/clasificacion';
import { constants } from 'src/app/util/constants';

@Component({
  selector: 'app-admin-clasificaciones',
  templateUrl: './admin-clasificaciones.component.html',
  styleUrls: ['./admin-clasificaciones.component.css']
})
export class AdminClasificacionesComponent implements OnInit {

  clasificaciones: Clasificacion[];
  categorias: Categoria[];
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  title: string;
  controlCategorias: FormControl;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  selected: number;
  constructor(private clasificacionService: ClasificacionService,
    private alertService: MesaggesManagerService,
    private modalService: NgbModal,
    private categoriaService: CategoriaService) {
    this.categorias = [];
    this.controlCategorias = new FormControl([]);
    this.title = "Clasificaciones";
    this.obtenerCategorias();
  }

  ngOnInit() {
  }

  obtenerClasificaciones(categoriaId) {
    this.clasificacionService.getClasificaciones(categoriaId).subscribe(
      resp => {
        this.clasificaciones = resp;
      }, err => {
        console.error(err);
      }
    );
  }

  agregar(): void {
    const component = this.modalService.open(EditarClasificacionComponent).componentInstance;
    component.completo.subscribe(res => {
      if (res.des) {
        this.selected = res.selec;
        this.obtenerClasificaciones(res.selec);
        const clasificacion = new Clasificacion();
        clasificacion.descripcion = res.des;
        clasificacion.categoriaId = res.selec;
        this.clasificacionService.post(clasificacion).subscribe(res => {
          this.alertService.
            showSimpleMessage(constants.successTitle, constants.success, constants.successCreate);
          clasificacion.clasificacionId = res;
          this.agregarChip(clasificacion);
        }, err => {
          this.alertService.
            showSimpleMessage(constants.errorTitle, constants.error, constants.errorCreate);
        });
      }
    });
  }

  elminar(clasificacion: Clasificacion): void {
    this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDelete).subscribe(
      resp => {
        if (resp) {
          this.clasificacionService.delete(clasificacion.clasificacionId).subscribe(
            resp => {
              this.alertService.
                showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
              this.eliminarChip(clasificacion);
            }, err => {
              if (err == constants.enUso) {
                this.alertService.
                  showSimpleMessage(constants.errorTitle, constants.error, constants.errorEnUso);
              } else {
                this.alertService.
                  showSimpleMessage(constants.errorTitle, constants.error, constants.errorDelete);
              }
            }
          );
        }
      }
    );
  }

  editar(clasificacion) {
    const component = this.modalService.open(EditarClasificacionComponent).componentInstance;
    component.control = new FormControl(clasificacion.descripcion);
    component.controlCategorias = new FormControl(clasificacion.categoriaId)
    component.completo.subscribe(res => {
      if (res.des) {
        this.selected = res.selec;
        clasificacion.descripcion = res.des;
        clasificacion.categoriaId = res.selec;
        this.clasificacionService.put(clasificacion).subscribe(res => {
          this.editarChip(clasificacion);
          this.obtenerClasificaciones(this.selected);
        }, err => {
          this.alertService.
            showSimpleMessage(constants.errorTitle, constants.error, constants.errorUpdate);
        });
      }
    });
  }

  agregarChip(clasificacion: Clasificacion) {

    this.clasificaciones.push(clasificacion);
  }

  eliminarChip(clasificacion) {
    const index = this.clasificaciones.indexOf(clasificacion);

    if (index >= 0) {
      this.clasificaciones.splice(index, 1);
    }
  }

  editarChip(clasificacion) {
    const documentoReal = this.clasificaciones.find(d => d.clasificacionId == clasificacion.clasificacionId);
    documentoReal.descripcion = clasificacion.descripcion;
  }

  obtenerCategorias() {
    this.categoriaService.get().subscribe(
      resp => {
        this.categorias = resp;
        this.controlCategorias = new FormControl(this.categorias);
      }, err => {
        console.error(err);
      }
    );
  }
}
