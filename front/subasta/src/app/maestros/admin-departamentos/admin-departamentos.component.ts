import { Component, OnInit } from '@angular/core';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { Departamento } from 'src/app/dtos/departamento';
import { DepartamentoService } from 'src/app/services/departamento-service.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MatChipInputEvent } from '@angular/material';
import { constants } from 'src/app/util/constants';
import { EditarSimpleComponent } from '../editar-simple/editar-simple.component';
import { FormControl } from '@angular/forms';

export interface Fruit {
  name: string;
}
@Component({
  selector: 'app-admin-departamentos',
  templateUrl: './admin-departamentos.component.html',
  styleUrls: ['./admin-departamentos.component.css']
})
export class AdminDepartamentosComponent implements OnInit {

  departamentos: Departamento[];
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  title: string;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];

  constructor(private departamentoService: DepartamentoService,
    private alertService: MesaggesManagerService,
    private modalService: NgbModal) {
    this.title = "Departamentos";
    this.obtenerDepartamentos();
  }

  ngOnInit() {
  }

  obtenerDepartamentos() {
    this.departamentoService.getDepartamentos().subscribe(
      resp => {
        this.departamentos = resp;
      }, err => {
        console.error(err);
      }
    )
  }


  agregar(event: MatChipInputEvent): void {
    const value = event.value;
    if ((value || '').trim() && this.validarSiExiste(value)) {
      const departamento = new Departamento();
      departamento.descripcion = value;
      this.departamentoService.post(departamento).subscribe(res => {
        departamento.departamentoId = res;
        this.alertService.
          showSimpleMessage(constants.successTitle, constants.success, constants.successCreate);
        this.agregarChip(event, departamento);
      }, err => {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorCreate);
        console.error(err);
      });
    }
  }

  elminar(departamento: Departamento): void {
    this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDelete).subscribe(
      resp => {
        if (resp) {
          this.departamentoService.delete(departamento.departamentoId).subscribe(
            resp => {
              this.alertService.
                showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
              this.eliminarChip(departamento);
            }, err => {
              debugger;
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
        this.departamentoService.put(documento).subscribe(res => {
          this.editarChip(documento);
        }, err => {
          this.alertService.
            showSimpleMessage(constants.errorTitle, constants.error, constants.errorUpdate);
        });
      }
    });
  }

  agregarChip(event: MatChipInputEvent, departamento: Departamento) {
    const input = event.input;

    this.departamentos.push(departamento);

    if (input) {
      input.value = '';
    }
  }

  eliminarChip(documento) {
    const index = this.departamentos.indexOf(documento);

    if (index >= 0) {
      this.departamentos.splice(index, 1);
    }
  }

  editarChip(documento) {
    const documentoReal = this.departamentos.find(d => d.departamentoId == documento.departamentoId);
    documentoReal.descripcion = documento.descripcion;
  }

  validarSiExiste(descripcion) {
    const dto = this.departamentos.find(d => d.descripcion.toLowerCase() == descripcion.toLowerCase());
    if (dto) {
      return false;
    }
    return true;
  }

}
