import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CrearPropietarioComponent } from './crear-propietario/crear-propietario';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalMessageComponent } from '../modal-message/modal-message.component';
import { DetallesComponent } from '../detalles/detalles.component';
import { ListarPropietariosComponent } from './listar-propietario/listar-propietarios.component';
import { ListarClienteComponent } from './listar-cliente/listar-cliente.component';

@NgModule({
  declarations: [CrearPropietarioComponent, ListarPropietariosComponent, ListarClienteComponent],
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    FormsModule, ReactiveFormsModule, BrowserModule, BrowserAnimationsModule
  ],
  exports: [
    CrearPropietarioComponent, ListarPropietariosComponent
  ],
  bootstrap: [],
  entryComponents: [ModalMessageComponent, DetallesComponent]
})
export class ClientesModule { }
