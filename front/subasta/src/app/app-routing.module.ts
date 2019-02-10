import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListarClientesComponent } from './clientes/listar-clientes/listar-clientes.component';
import { CrearPropietarioComponent } from './clientes/crear-propietario/crear-propietario';
import { LoginComponent } from './login/login.component';
import { AutenticacionService } from './services/autenticacion/autenticacion.service';
import { RoleguardService } from './services/autenticacion/roleguard.service';
import { CrearEventoComponent } from './eventos/crear-evento/crear-evento.component';
import { ListarEventosComponent } from './eventos/listar-eventos/listar-eventos.component';
import { CrearLotesComponent } from './lotes/crear-lotes/crear-lotes.component';
import { ListarLotesComponent } from './lotes/listar-lotes/listar-lotes.component';
import { CrearComponent } from './animales/crear/crear.component';
import { ListarComponent } from './animales/listar/listar.component';
import { SubastasComponent } from './subastas/subastas/subastas.component';
import { CrearPujadorComponent } from './pujadores/crear/crear.component';
import { ListarPujadorComponent } from './pujadores/listar/listar.component';
import { CrearSubastaComponent } from './subastas/crear-subasta/crear-subasta.component';
import { ListarSubastaComponent } from './subastas/listar-subasta/listar-subasta.component';
import { CrearSubastaNuevaComponent } from './subastas/crear-subasta-nueva/crear-subasta-nueva.component';

const routes: Routes = [
  {
    path: 'listar-propietario', component: ListarClientesComponent, canActivate: [AutenticacionService, RoleguardService],
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
  {
    path: 'crear-evento', component: CrearEventoComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'crear-evento:/id', component: CrearEventoComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'listar-evento', component: ListarEventosComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'listar-lote', component: ListarLotesComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'crear-lote', component: CrearLotesComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'crear-animal', component: CrearComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'crear-animal/:id', component: CrearComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'listar-animal', component: ListarComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'listar-pujador', component: ListarPujadorComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'crear-pujador', component: CrearPujadorComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'crear-pujador/:id', component: CrearPujadorComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'listar-subasta', component: ListarSubastaComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'crear-subasta', component: CrearSubastaNuevaComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'crear-subasta/:id', component: CrearSubastaNuevaComponent, canActivate: [AutenticacionService, RoleguardService],
    data: {
      expectedRole: 'Administrador'
    }
  },
  {
    path: 'subastas', component: SubastasComponent},
  { path: 'login', component: LoginComponent },
  { path: '**', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
