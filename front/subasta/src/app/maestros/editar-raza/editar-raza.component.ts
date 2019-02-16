import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Categoria } from 'src/app/dtos/categoria';
import { FormControl } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CategoriaService } from 'src/app/services/categoria.service';

@Component({
  selector: 'app-editar-raza',
  templateUrl: './editar-raza.component.html',
  styleUrls: ['./editar-raza.component.css']
})
export class EditarRazaComponent implements OnInit {

  @Output() completo: EventEmitter<any> = new EventEmitter();
  categorias: Categoria[];
  control: FormControl;
  controlCategorias: FormControl;
  title: string;
  selected: number;
  constructor(public activeModal: NgbActiveModal,
    private categoriaService: CategoriaService) {
    this.title = "Crear raza";
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
