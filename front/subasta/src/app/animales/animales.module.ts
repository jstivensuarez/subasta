import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CrearComponent } from './crear/crear.component';
import { ListarComponent } from './listar/listar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalMessageComponent } from '../modal-message/modal-message.component';
import { DetallesComponent } from '../detalles/detalles.component';
import { CustomWeightMaskConfig } from '../util/numberMaskConfig';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { DataTableModule } from 'angular-6-datatable';
import { CURRENCY_MASK_CONFIG } from 'ng2-currency-mask/src/currency-mask.config';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    FormsModule, ReactiveFormsModule, 
    BrowserModule, 
    BrowserAnimationsModule,
    CurrencyMaskModule,
    DataTableModule
  ],
  declarations: [CrearComponent, ListarComponent],
  exports: [CrearComponent, ListarComponent],
  entryComponents: [ModalMessageComponent, DetallesComponent],
  providers: [{ provide: CURRENCY_MASK_CONFIG, useValue: CustomWeightMaskConfig }]
})
export class AnimalesModule { }
