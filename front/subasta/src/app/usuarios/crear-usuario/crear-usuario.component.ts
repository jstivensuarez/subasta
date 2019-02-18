import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Validation } from 'src/app/util/validations';
import { Usuario } from 'src/app/dtos/usuario';
import { UsuarioService } from 'src/app/services/usuario.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { constants } from 'src/app/util/constants';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-crear-usuario',
  templateUrl: './crear-usuario.component.html',
  styleUrls: ['./crear-usuario.component.css']
})
export class CrearUsuarioComponent implements OnInit {

  form: FormGroup;
  usuarioDto: Usuario;
  title: string;
  isEditing: boolean;
  constructor(private usuarioService: UsuarioService,
    private alertService: MesaggesManagerService,
    private router: Router,
    private route: ActivatedRoute) {
    this.title = "Usuarios";
    this.isEditing = false;
    this.usuarioDto = new Usuario();
    this.verificarUrl();
    this.form = this.createFormRegister();
  }

  ngOnInit() {
  }

  verificarUrl() {
    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        this.isEditing = true;
        this.title = 'Editar usuario';
        this.obtenerUsuario(params['id']);
      } else {
        this.isEditing = false;
        this.usuarioDto = new Usuario();
        this.form = this.createFormRegister();
        this.title = 'Crear usuario';
      }
    });
  }

  obtenerUsuario(id: string) {
    this.usuarioService.getDto(id).subscribe(res => {
      this.usuarioDto = res;
      this.form = this.createFormRegister();
    }, err => {
      console.error(err);
    });
  }

  onSubmit() {
    const usuario = new Usuario();
    usuario.nombre = this.usuario.value;
    usuario.correo = this.correo.value;
    if (this.isEditing) {
      this.editarUsuario(usuario);
    } else {
      this.crearUsuario(usuario);
    }

  }

  validarCliente(nombre) {
    if (nombre) {
      this.usuarioService.validate(nombre).subscribe(res => {
        this.usuario.setErrors({});
        this.usuario.updateValueAndValidity({
          onlySelf: true
        });
      }, err => {
        this.usuario.setErrors({ 'invalid': true });
      });
    } else {
      this.usuario.setErrors({});
      this.usuario.updateValueAndValidity({
        onlySelf: true
      });
    }
  }

  crearUsuario(cliente: Usuario) {
    this.usuarioService.post(cliente).subscribe(res => {
      this.alertService.
        showSimpleMessage(constants.successTitle, constants.success, constants.successCreate);
      this.regresar();
    }, err => {
      if (err === constants.alreadyExist) {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorAlreadyExists);
      } else {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorCreate);
      }
      console.error(err);
    });
  }

  editarUsuario(cliente: Usuario) {
    this.usuarioService.put(cliente).subscribe(res => {
      this.alertService.
        showSimpleMessage(constants.successTitle, constants.success, constants.successUpdate);
      this.regresar();
    }, err => {
      this.alertService.
        showSimpleMessage(constants.errorTitle, constants.error, constants.errorUpdate);
      console.error(err);
    });
  }

  createFormRegister() {
    return new FormGroup({
      correo: new FormControl(this.usuarioDto.correo, [Validators.required, Validators.email]),
      usuario: new FormControl(this.usuarioDto.nombre, [Validators.required, Validators.minLength(8)])
    });
  }

  regresar() {
    this.router.navigate(['/listar-usuario']);
  }

  get usuario() {
    return this.form.get("usuario");
  }

  get correo() {
    return this.form.get("correo");
  }
}
