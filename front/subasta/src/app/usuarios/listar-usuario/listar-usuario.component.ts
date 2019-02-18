import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Usuario } from 'src/app/dtos/usuario';
import { UsuarioService } from 'src/app/services/usuario.service';
import { Router } from '@angular/router';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { constants } from 'src/app/util/constants';

@Component({
  selector: 'app-listar-usuario',
  templateUrl: './listar-usuario.component.html',
  styleUrls: ['./listar-usuario.component.css']
})
export class ListarUsuarioComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: MatTableDataSource<Usuario>;
  usuarios: Usuario[];
  displayedColumns: string[] = ['usuario', 'correo', 'acciones'];
  title: string;
  constructor(
    private subastaService: UsuarioService,
    private router: Router,
    private alertService: MesaggesManagerService,
  ) {
    this.title = "Usuarios";
    this.obtenerUsuarios();
  }

  ngOnInit() {
  }

  eliminar(usuario) {
    if (this.usuarios.length > 1) {
      this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDelete).subscribe(
        resp => {
          if (resp) {
            this.subastaService.delete(usuario.usuarioId).subscribe(
              resp => {
                this.alertService.
                  showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
                this.obtenerUsuarios();
              }, err => {
                this.alertService.
                  showSimpleMessage(constants.errorTitle, constants.error, constants.errorDelete);
              }
            );
          }
        }
      );
    }
  }

  editar(usuario) {
    this.router.navigate(['/crear-usuario'], { queryParams: { id: usuario.usuarioId } });
  }

  agregar() {
    this.router.navigate(['/crear-usuario']);
  }

  obtenerUsuarios() {
    this.subastaService.get().subscribe(resp => {
      this.usuarios = resp;
      this.dataSource = new MatTableDataSource(resp);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }, err => {
      console.error(err);
    });
  }

  applyFilter(filtro: string) {
    const subastasFiltrados = this.usuarios.filter(
      option => option.correo.toLowerCase().indexOf(filtro.toLowerCase()) === 0 ||
        option.correo.toLowerCase().indexOf(filtro.toLowerCase()) > 0 ||
        option.nombre.toString().toLowerCase().lastIndexOf(filtro.toLowerCase()) === 0 ||
        option.nombre.toString().toLowerCase().lastIndexOf(filtro.toLowerCase()) > 0);
    this.dataSource.data = subastasFiltrados;
    this.dataSource.paginator.firstPage();
  }
}
