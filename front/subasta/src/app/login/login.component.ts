import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Usuario } from '../dtos/usuario';
import { UsuarioService } from '../services/usuario.service';
import { MesaggesManagerService } from '../services/mesagges-manager.service';
import { constants } from '../util/constants';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  form: FormGroup;
  usuario: Usuario;
  constructor(
    private usuarioService: UsuarioService,
    private alertService: MesaggesManagerService
  ) {
    this.usuario = new Usuario();
    this.form = this.createForm();
  }

  ngOnInit() {
  }

  onSubmit() {
    const usuario = new Usuario();
    usuario.clave = this.clave.value;
    usuario.correo = this.ingreso.value;
    usuario.nombre = this.ingreso.value;
    this.login(usuario);
  }

  login(usuario){
    this.usuarioService.login(usuario).subscribe(
      res => {
        localStorage.setItem('token', res.token);
        this.usuarioService.redirectToMenu();          
      }, err => {
          this.alertService.
            showSimpleMessage(constants.errorTitle, constants.error, constants.errorLogin);
        console.error(err);
      }
    );
  }
  
  createForm() {
    return new FormGroup({
      ingreso: new FormControl(this.usuario.correo, [Validators.required]),
      clave: new FormControl(this.usuario.clave, [Validators.required, Validators.min(8)]),
    });
  }

  get ingreso() {
    return this.form.get('ingreso');
  }

  get clave() {
    return this.form.get('clave');
  }
}
