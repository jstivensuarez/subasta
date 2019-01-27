import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListarClientesComponent } from './clientes/listar-clientes/listar-clientes.component';
import { CrearPropietarioComponent } from './clientes/crear-propietario/crear-propietario';
import { LoginComponent } from './login/login.component';
import { AutenticacionService } from './services/autenticacion/autenticacion.service';
import { RoleguardService } from './services/autenticacion/roleguard.service';

const routes: Routes = [
  {
    path: 'listar-cliente', component: ListarClientesComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'crear-propietario', component: CrearPropietarioComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'crear-propietario/:id', component: CrearPropietarioComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  { path: 'login', component: LoginComponent },
  { path: '**', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
