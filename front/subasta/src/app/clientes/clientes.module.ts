import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ListarClientesComponent } from './listar-clientes/listar-clientes.component';
import { CrearPropietarioComponent } from './crear-propietario/crear-propietario.propietarios';

@NgModule({
  declarations: [CrearPropietarioComponent, ListarClientesComponent],
  imports: [
    CommonModule, MaterialModule, 
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    CrearPropietarioComponent, ListarClientesComponent
  ]
})
export class ClientesModule { }
