import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DepartamentoService } from 'src/app/services/departamento-service.service';
import { Departamento } from 'src/app/dtos/departamento';

@Component({
  selector: 'app-editar-avanzado',
  templateUrl: './editar-avanzado.component.html',
  styleUrls: ['./editar-avanzado.component.css']
})
export class EditarAvanzadoComponent implements OnInit {

  @Output() completo: EventEmitter<any> = new EventEmitter();
  departamentos: Departamento[];
  control: FormControl;
  controlDepartamentos: FormControl;
  title: string;
  selected: number;
  constructor(public activeModal: NgbActiveModal,
    private deparService: DepartamentoService) {
    this.title = "Crear municipio";
    this.departamentos = [];
    this.control = new FormControl('');
    this.controlDepartamentos = new FormControl(this.selected);
    this.obtenerDepartamentos();
  }

  ngOnInit() {
  }

  guardar() {
    this.completo.emit({ des: this.control.value, selec: this.controlDepartamentos.value});
    this.activeModal.close();
  }

  cancel() {
    this.activeModal.close();
  }

  obtenerDepartamentos() {
    this.deparService.getDepartamentos().subscribe(
      resp => {
        this.departamentos = resp;
      }, err => {
        console.error(err);
      }
    );
  }
}
