import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { CrearSubastaComponent } from './crear-subasta/crear-subasta.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { CustomCurrencyMaskConfig } from '../util/numberMaskConfig';
import { CURRENCY_MASK_CONFIG } from 'ng2-currency-mask/src/currency-mask.config';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    FormsModule, ReactiveFormsModule, BrowserModule, BrowserAnimationsModule, CurrencyMaskModule
  ],
  declarations: [CrearSubastaComponent],
  exports: [CrearSubastaComponent],
  providers:[DatePipe,  { provide: CURRENCY_MASK_CONFIG, useValue: CustomCurrencyMaskConfig }]
})
export class SubastasModule { }
