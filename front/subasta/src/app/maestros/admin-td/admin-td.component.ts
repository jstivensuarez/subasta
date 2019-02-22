import { Component, OnInit } from '@angular/core';
import { TipoDocumento } from 'src/app/dtos/tipo-documento';
import { TdServiceService } from 'src/app/services/td-service.service';
import { v } from '@angular/core/src/render3';
import { MatChipInputEvent } from '@angular/material';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { constants } from 'src/app/util/constants';
import { EditarSimpleComponent } from '../editar-simple/editar-simple.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-admin-td',
  templateUrl: './admin-td.component.html',
  styleUrls: ['./admin-td.component.css']
})
export class AdminTdComponent implements OnInit {

  documentos: TipoDocumento[];
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  title: string;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];

  constructor(private tdService: TdServiceService,
    private alertService: MesaggesManagerService,
    private modalService: NgbModal) {
    this.title = "Tipos de documentos";
    this.obtenerTipoDocumento();
  }

  ngOnInit() {
  }

  obtenerTipoDocumento() {
    this.tdService.getTipoDocumentos().subscribe(
      resp => {
        this.documentos = resp;
      }, err => {
        console.error(err);
      }
    )
  }


  agregar(event: MatChipInputEvent): void {
    const value = event.value;
    if ((value || '').trim() && this.validarSiExiste(value)) {
      const td = new TipoDocumento();
      td.descripcion = value;
      this.tdService.post(td).subscribe(res => {
        td.tipoDocumentoId = res;
        this.alertService.
          showSimpleMessage(constants.successTitle, constants.success, constants.successCreate);
        this.agregarChip(event, td);
      }, err => {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorCreate);
        console.error(err);
      });
    }
  }

  elminar(documento: TipoDocumento): void {
    this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDelete).subscribe(
      resp => {
        if (resp) {
          this.tdService.delete(documento.tipoDocumentoId).subscribe(
            resp => {
              this.alertService.
                showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
              this.eliminarChip(documento);
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
        this.tdService.put(documento).subscribe(res => {
          this.editarChip(documento);
        }, err => {
          this.alertService.
            showSimpleMessage(constants.errorTitle, constants.error, constants.errorUpdate);

        });
      }
    });
  }

  agregarChip(event: MatChipInputEvent, documento: TipoDocumento) {
    const input = event.input;

    this.documentos.push(documento);

    if (input) {
      input.value = '';
    }
  }

  eliminarChip(documento) {
    const index = this.documentos.indexOf(documento);

    if (index >= 0) {
      this.documentos.splice(index, 1);
    }
  }

  editarChip(documento) {
    const documentoReal = this.documentos.find(d => d.tipoDocumentoId == documento.tipoDocumentoId);
    documentoReal.descripcion = documento.descripcion;
  }

  validarSiExiste(descripcion) {
    const dto = this.documentos.find(d => d.descripcion.toLowerCase() == descripcion.toLowerCase());
    if (dto) {
      return false;
    }
    return true;
  }
}


