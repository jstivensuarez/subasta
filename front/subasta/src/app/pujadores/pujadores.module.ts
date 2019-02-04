import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListarComponent } from './listar/listar.component';
import { CrearComponent } from './crear/crear.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [ListarComponent, CrearComponent]
})
export class PujadoresModule { }
