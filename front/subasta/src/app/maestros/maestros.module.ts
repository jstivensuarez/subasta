import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminCategoriasComponent } from './admin-categorias/admin-categorias.component';
import { AdminRazasComponent } from './admin-razas/admin-razas.component';
import { AdminClasificacionesComponent } from './admin-clasificaciones/admin-clasificaciones.component';
import { AdminTdComponent } from './admin-td/admin-td.component';
import { AdminDepartamentosComponent } from './admin-departamentos/admin-departamentos.component';
import { AdminCiudadesComponent } from './admin-ciudades/admin-ciudades.component';
import { MaterialModule } from '../material/material.module';
import { EditarSimpleComponent } from './editar-simple/editar-simple.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EditarAvanzadoComponent } from './editar-avanzado/editar-avanzado.component';
import { EditarRazaComponent } from './editar-raza/editar-raza.component';
import { EditarClasificacionComponent } from './editar-clasificacion/editar-clasificacion.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    FormsModule, ReactiveFormsModule, BrowserModule, BrowserAnimationsModule
  ],
  declarations: [AdminCategoriasComponent, AdminRazasComponent,
    AdminClasificacionesComponent, AdminTdComponent, AdminDepartamentosComponent,
     AdminCiudadesComponent, EditarSimpleComponent, EditarAvanzadoComponent, 
     EditarRazaComponent, EditarClasificacionComponent],
  entryComponents: [EditarSimpleComponent, EditarAvanzadoComponent,
    EditarRazaComponent, EditarClasificacionComponent]
})
export class MaestrosModule { }
