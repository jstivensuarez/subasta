import { Component, OnInit } from '@angular/core';
import { AnimalService } from 'src/app/services/animal.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { ActivatedRoute, Router } from '@angular/router';
import { constants } from 'src/app/util/constants';
import { Animal } from 'src/app/dtos/animal';

@Component({
  selector: 'app-listar',
  templateUrl: './listar.component.html',
  styleUrls: ['./listar.component.css']
})
export class ListarComponent implements OnInit {

  animales: Animal[];
  displayedColumns: string[] = ['descripcion', 'peso', 'categoria', 'raza', 'sexo','lote', 'acciones'];
  title: string;
  constructor(private animalService: AnimalService,
    private alertService: MesaggesManagerService,
    private route: ActivatedRoute,
    private router: Router) {
      this.obtenerAnimales();
     }

  ngOnInit() {
  }

  eliminar(animal) {
    this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDelete).subscribe(
      resp => {
        if (resp) {
          this.animalService.delete(animal.animalId).subscribe(
            resp => {
              this.alertService.
                showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
              this.obtenerAnimales();
            }, err => {
              this.alertService.
                showSimpleMessage(constants.errorTitle, constants.error, constants.errorDelete);
            }
          );
        }
      }
    );
  }

  ver(animal) {
    debugger;
    let video = null;
    let imagen = null;
    if(/^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$/.test(animal.foto)){
      video = animal.foto;
    }else{
      imagen = 'ANIMALES/'+animal.foto;
    }
    this.alertService.showDetails('Detalles del animal', {
      Descripción: animal.descripcion,
      Peso: animal.peso+ '(Kg)',
      Categoría: animal.categoria.descripcion,
      Raza: animal.raza.descripcion,
      Sexo: animal.sexo.descripcion,
      Lote: animal.lote.descripcion,
      imagen: imagen,
      video: video,
    });
  }

  editar(animal) {
    this.router.navigate(['/crear-animal'], { queryParams: { id: animal.animalId } });
  }

  obtenerAnimales() {
    this.animalService.get().subscribe(
      resp => {
        this.animales = resp;
      }, err => {
        console.error(err);
      });
  }

  agregarAnimal() {
    this.router.navigate(['/crear-animal']);
  }
}
