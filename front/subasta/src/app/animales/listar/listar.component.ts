import { Component, OnInit, ViewChild } from '@angular/core';
import { AnimalService } from 'src/app/services/animal.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { ActivatedRoute, Router } from '@angular/router';
import { constants } from 'src/app/util/constants';
import { Animal } from 'src/app/dtos/animal';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-listar',
  templateUrl: './listar.component.html',
  styleUrls: ['./listar.component.css']
})
export class ListarComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: MatTableDataSource<Animal>;
  animales: Animal[];
  displayedColumns: string[] = ['descripcion', 'peso', 'sexo', 'lote', 'acciones'];
  title: string;
  constructor(private animalService: AnimalService,
    private alertService: MesaggesManagerService,
    private route: ActivatedRoute,
    private router: Router) {
    this.title = 'Animales';
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
    let video = null;
    let imagen = null;
    if (/^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$/.test(animal.foto)) {
      video = animal.foto;
    } else {
      imagen = 'ANIMALES/' + animal.foto;
    }
    this.alertService.showDetails('Detalles del animal', {
      Descripción: animal.descripcion,
      Peso: animal.peso + '(Kg)',
      Sexo: animal.sexo.descripcion,
      Lote: animal.lote.nombre,
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
        this.dataSource = new MatTableDataSource(resp);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }, err => {
        console.error(err);
      });
  }

  applyFilter(filtro: string) {
    const animalesFiltrados = this.animales.filter(
      option => option.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.peso.toString().toLowerCase().lastIndexOf(filtro.toLowerCase()) === 0 ||
        option.peso.toString().toLowerCase().lastIndexOf(filtro.toLowerCase()) > 0 ||
        option.sexo.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.sexo.toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.lote.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.lote.descripcion.toLowerCase().indexOf(filtro.toLowerCase()) > 0);
    this.dataSource.data = animalesFiltrados;
    this.dataSource.paginator.firstPage();
  }

  agregarAnimal() {
    this.router.navigate(['/crear-animal']);
  }
}
