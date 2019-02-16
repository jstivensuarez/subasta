import { Component, OnInit } from '@angular/core';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Municipio } from 'src/app/dtos/municipio';
import { MunicipioService } from 'src/app/services/municipio.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { MatChipInputEvent } from '@angular/material';
import { constants } from 'src/app/util/constants';
import { EditarAvanzadoComponent } from '../editar-avanzado/editar-avanzado.component';
import { FormControl } from '@angular/forms';
import { DepartamentoService } from 'src/app/services/departamento-service.service';
import { Departamento } from 'src/app/dtos/departamento';

@Component({
  selector: 'app-admin-ciudades',
  templateUrl: './admin-ciudades.component.html',
  styleUrls: ['./admin-ciudades.component.css']
})
export class AdminCiudadesComponent implements OnInit {

  municipios: Municipio[];
  departamentos: Departamento[];
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  title: string;
  controlDepartamentos: FormControl;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  selected: number;
  constructor(private municipioService: MunicipioService,
    private alertService: MesaggesManagerService,
    private modalService: NgbModal,
    private departamentoService: DepartamentoService) {
    this.departamentos = [];
    this.controlDepartamentos = new FormControl([]);
    this.title = "Municipios";
    this.obtenerDepartamentos();
  }

  ngOnInit() {
  }

  obtenerMunicipios(departamentoId) {
    this.municipioService.getMunicipios(departamentoId).subscribe(
      resp => {
        this.municipios = resp;
      }, err => {
        console.error(err);
      }
    );
  }

  agregar(): void {
    const component = this.modalService.open(EditarAvanzadoComponent).componentInstance;
    component.completo.subscribe(res => {
      this.selected = res.selec;
      this.obtenerMunicipios(res.selec);
      const municipio = new Municipio();
      municipio.descripcion = res.des;
      municipio.departamentoId = res.selec;
      this.municipioService.post(municipio).subscribe(res => {
        this.alertService.
          showSimpleMessage(constants.successTitle, constants.success, constants.successCreate);
        municipio.municipioId = res;
        this.agregarChip(municipio);
      }, err => {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorCreate);
      });
    });
  }

  elminar(municipio: Municipio): void {
    this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDelete).subscribe(
      resp => {
        if (resp) {
          this.municipioService.delete(municipio.municipioId).subscribe(
            resp => {
              this.alertService.
                showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
              this.eliminarChip(municipio);
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

  editar(municipio) {
    const component = this.modalService.open(EditarAvanzadoComponent).componentInstance;
    component.control = new FormControl(municipio.descripcion);
    component.controlDepartamentos = new FormControl(municipio.departamentoId)
    component.completo.subscribe(res => {
      this.selected = res.selec;
      municipio.descripcion = res.des;
      municipio.departamentoId = res.selec;
      this.municipioService.put(municipio).subscribe(res => {
        this.editarChip(municipio);
        this.obtenerMunicipios(this.selected);
      }, err => {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorUpdate);
      });
    });
  }

  agregarChip(municipio: Municipio) {

    this.municipios.push(municipio);
  }

  eliminarChip(ciudad) {
    const index = this.municipios.indexOf(ciudad);

    if (index >= 0) {
      this.municipios.splice(index, 1);
    }
  }

  editarChip(ciudad) {
    const ciudadReal = this.municipios.find(d => d.municipioId == ciudad.municipioId);
    ciudadReal.descripcion = ciudad.descripcion;
  }

  obtenerDepartamentos() {
    this.departamentoService.getDepartamentos().subscribe(
      resp => {
        this.departamentos = resp;
        this.controlDepartamentos = new FormControl(this.departamentos);
      }, err => {
        console.error(err);
      }
    );
  }
}
