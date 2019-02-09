import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListarPujadorComponent } from './listar/listar.component';
import { CrearPujadorComponent } from './crear/crear.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { CURRENCY_MASK_CONFIG } from 'ng2-currency-mask/src/currency-mask.config';
import { CustomCurrencyMaskConfig } from '../util/numberMaskConfig';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    FormsModule, ReactiveFormsModule, BrowserModule, BrowserAnimationsModule, CurrencyMaskModule
  ],
  declarations: [ListarPujadorComponent, CrearPujadorComponent],
  providers:[{ provide: CURRENCY_MASK_CONFIG, useValue: CustomCurrencyMaskConfig }]
})
export class PujadoresModule { }
