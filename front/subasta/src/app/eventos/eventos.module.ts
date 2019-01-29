import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CrearEventoComponent } from './crear-evento/crear-evento.component';
import { MaterialModule } from '../material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SubastasModule } from '../subastas/subastas.module';
import { ListarEventosComponent } from './listar-eventos/listar-eventos.component';
import { CrearSubastaComponent } from '../subastas/crear-subasta/crear-subasta.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    FormsModule, ReactiveFormsModule, BrowserModule, BrowserAnimationsModule, SubastasModule
  ],
  declarations: [CrearEventoComponent, ListarEventosComponent],
  entryComponents: [CrearSubastaComponent]
})
export class EventosModule { }
