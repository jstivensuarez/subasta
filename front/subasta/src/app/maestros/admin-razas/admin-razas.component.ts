import { Component, OnInit } from '@angular/core';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { Raza } from 'src/app/dtos/raza';
import { Categoria } from 'src/app/dtos/categoria';
import { FormControl } from '@angular/forms';
import { RazaService } from 'src/app/services/raza.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CategoriaService } from 'src/app/services/categoria.service';
import { EditarRazaComponent } from '../editar-raza/editar-raza.component';
import { constants } from 'src/app/util/constants';

@Component({
  selector: 'app-admin-razas',
  templateUrl: './admin-razas.component.html',
  styleUrls: ['./admin-razas.component.css']
})
export class AdminRazasComponent implements OnInit {

  razas: Raza[];
  categorias: Categoria[];
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  title: string;
  controlCategorias: FormControl;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  selected: number;
  constructor(private razaService: RazaService,
    private alertService: MesaggesManagerService,
    private modalService: NgbModal,
    private categoriaService: CategoriaService) {
    this.categorias = [];
    this.controlCategorias = new FormControl([]);
    this.title = "Razas";
    this.obtenerCategorias();
  }

  ngOnInit() {
  }

  obtenerRazas(categoriaId) {
    this.razaService.getRazas(categoriaId).subscribe(
      resp => {
        this.razas = resp;
      }, err => {
        console.error(err);
      }
    );
  }

  agregar(): void {
    const component = this.modalService.open(EditarRazaComponent).componentInstance;
    component.completo.subscribe(res => {
      this.selected = res.selec;
      this.obtenerRazas(res.selec);
      const raza = new Raza();
      raza.descripcion = res.des;
      raza.categoriaId = res.selec;
      this.razaService.post(raza).subscribe(res => {
        this.alertService.
          showSimpleMessage(constants.successTitle, constants.success, constants.successCreate);
        raza.razaId = res;
        this.agregarChip(raza);
      }, err => {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorCreate);
      });
    });
  }

  elminar(raza: Raza): void {
    this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDelete).subscribe(
      resp => {
        if (resp) {
          this.razaService.delete(raza.razaId).subscribe(
            resp => {
              this.alertService.
                showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
              this.eliminarChip(raza);
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

  editar(raza) {
    const component = this.modalService.open(EditarRazaComponent).componentInstance;
    component.control = new FormControl(raza.descripcion);
    component.controlCategorias = new FormControl(raza.categoriaId)
    component.completo.subscribe(res => {
      this.selected = res.selec;
      raza.descripcion = res.des;
      raza.categoriaId = res.selec;
      this.razaService.put(raza).subscribe(res => {
        this.editarChip(raza);
        this.obtenerRazas(this.selected);
      }, err => {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorUpdate);
      });
    });
  }

  agregarChip(raza: Raza) {

    this.razas.push(raza);
  }

  eliminarChip(raza) {
    const index = this.razas.indexOf(raza);

    if (index >= 0) {
      this.razas.splice(index, 1);
    }
  }

  editarChip(raza) {
    const razaReal = this.razas.find(d => d.razaId == raza.razaId);
    razaReal.descripcion = raza.descripcion;
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
