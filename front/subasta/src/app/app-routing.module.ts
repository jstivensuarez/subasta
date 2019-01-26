import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListarClientesComponent } from './clientes/listar-clientes/listar-clientes.component';
import { CrearPropietarioComponent } from './clientes/crear-propietario/crear-propietario.propietarios';

const routes: Routes = [
  { path: 'listar-cliente', component: ListarClientesComponent },
  { path: 'crear-propietario', component: CrearPropietarioComponent },
  { path: 'crear-propietario/:id', component: CrearPropietarioComponent },
  { path: '**', component: ListarClientesComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
