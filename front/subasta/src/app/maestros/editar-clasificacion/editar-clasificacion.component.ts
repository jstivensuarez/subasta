import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Clasificacion } from 'src/app/dtos/clasificacion';
import { FormControl } from '@angular/forms';
import { Categoria } from 'src/app/dtos/categoria';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CategoriaService } from 'src/app/services/categoria.service';

@Component({
  selector: 'app-editar-clasificacion',
  templateUrl: './editar-clasificacion.component.html',
  styleUrls: ['./editar-clasificacion.component.css']
})
export class EditarClasificacionComponent implements OnInit {

  @Output() completo: EventEmitter<any> = new EventEmitter();
  categorias: Categoria[];
  control: FormControl;
  controlCategorias: FormControl;
  title: string;
  selected: number;
  constructor(public activeModal: NgbActiveModal,
    private categoriaService: CategoriaService) {
    this.title = "Crear clasificación";
    this.categorias = [];
    this.control = new FormControl('');
    this.controlCategorias = new FormControl(this.selected);
    this.obtenerCategorias();
  }

  ngOnInit() {
  }

  guardar() {
    this.completo.emit({ des: this.control.value, selec: this.controlCategorias.value});
    this.activeModal.close();
  }

  cancel() {
    this.activeModal.close();
  }

  obtenerCategorias() {
    this.categoriaService.get().subscribe(
      resp => {
        this.categorias = resp;
      }, err => {
        console.error(err);
      }
    );
  }

}
