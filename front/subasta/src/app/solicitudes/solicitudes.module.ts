import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListarSolicitudesComponent } from './listar-solicitudes/listar-solicitudes.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { VerSolicitudComponent } from './ver-solicitud/ver-solicitud.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    FormsModule, ReactiveFormsModule, BrowserModule, BrowserAnimationsModule
  ],
  declarations: [ListarSolicitudesComponent, VerSolicitudComponent],
  entryComponents: [VerSolicitudComponent]
})
export class SolicitudesModule { }
