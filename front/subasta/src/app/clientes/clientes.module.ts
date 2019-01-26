import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ListarClientesComponent } from './listar-clientes/listar-clientes.component';
import { CrearPropietarioComponent } from './crear-propietario/crear-propietario';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalMessageComponent } from '../modal-message/modal-message.component';

@NgModule({
  declarations: [CrearPropietarioComponent, ListarClientesComponent],
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    FormsModule, ReactiveFormsModule, BrowserModule, BrowserAnimationsModule
  ],
  exports: [
    CrearPropietarioComponent, ListarClientesComponent
  ],
  bootstrap: [],
  entryComponents: [ModalMessageComponent]
})
export class ClientesModule { }
