import { Component, OnInit } from '@angular/core';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { Categoria } from 'src/app/dtos/categoria';
import { CategoriaService } from 'src/app/services/categoria.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MatChipInputEvent } from '@angular/material';
import { constants } from 'src/app/util/constants';
import { FormControl } from '@angular/forms';
import { EditarSimpleComponent } from '../editar-simple/editar-simple.component';

export interface Fruit {
  name: string;
}

@Component({
  selector: 'app-admin-categorias',
  templateUrl: './admin-categorias.component.html',
  styleUrls: ['./admin-categorias.component.css']
})
export class AdminCategoriasComponent implements OnInit {

  categorias: Categoria[];
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  title: string;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];

  constructor(private categoriaService: CategoriaService,
    private alertService: MesaggesManagerService,
    private modalService: NgbModal) {
    this.title = "Categorías";
    this.obtenerCategorias();
  }

  ngOnInit() {
  }

  obtenerCategorias() {
    this.categoriaService.get().subscribe(
      resp => {
        this.categorias = resp;
      }, err => {
        console.error(err);
      }
    )
  }


  agregar(event: MatChipInputEvent): void {
    const value = event.value;
    if ((value || '').trim() && this.validarSiExiste(value)) {
      const categoria = new Categoria();
      categoria.descripcion = value;
      this.categoriaService.post(categoria).subscribe(res => {
        categoria.categoriaId = res;
        this.alertService.
          showSimpleMessage(constants.successTitle, constants.success, constants.successCreate);
        this.agregarChip(event, categoria);
      }, err => {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorCreate);
        console.error(err);
      });
    }
  }

  elminar(categoria: Categoria): void {
    this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDelete).subscribe(
      resp => {
        if (resp) {
          this.categoriaService.delete(categoria.categoriaId).subscribe(
            resp => {
              this.alertService.
                showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
              this.eliminarChip(categoria);
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

  editar(documento) {
    const component = this.modalService.open(EditarSimpleComponent).componentInstance;
    component.texto = documento.descripcion;
    component.control = new FormControl(documento.descripcion);
    component.completo.subscribe(res => {
      if (this.validarSiExiste(res)) {
        documento.descripcion = res;
        this.categoriaService.put(documento).subscribe(res => {
          this.editarChip(documento);
        }, err => {
          this.alertService.
            showSimpleMessage(constants.errorTitle, constants.error, constants.errorUpdate);
        });
      }
    });
  }

  agregarChip(event: MatChipInputEvent, categoria: Categoria) {
    const input = event.input;

    this.categorias.push(categoria);

    if (input) {
      input.value = '';
    }
  }

  eliminarChip(documento) {
    const index = this.categorias.indexOf(documento);

    if (index >= 0) {
      this.categorias.splice(index, 1);
    }
  }

  editarChip(documento) {
    const documentoReal = this.categorias.find(d => d.categoriaId == documento.categoriaId);
    documentoReal.descripcion = documento.descripcion;
  }

  validarSiExiste(descripcion) {
    const dto = this.categorias.find(d => d.descripcion.toLowerCase() == descripcion.toLowerCase());
    if (dto) {
      return false;
    }
    return true;
  }

}
